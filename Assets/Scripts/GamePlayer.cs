using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GamePlayer : MonoBehaviour
{

    public enum PlayerMode
    {
        nomal,
        god,
    }

    private Vector3 PlayerStartPosition;

    private bool isIgnoringWall = false;

    public PlayerMode playerMode = PlayerMode.nomal;
    public int Hp { get; private set; }
    public float moveSpeed = 5f;
    public float skillSpeedMultiplier = 1f; 
    private float Cooldown =0f;
    SkillIcon skillIcon;

    AudioSource audioSource;
    public AudioClip jumpClip;
    public AudioClip hurtclip;
    public AudioClip slideClip;

    public AudioClip skillOneClip;
    public AudioClip skillTwoClip;
    public AudioClip skillThreeClip;
    public float CurrentSpeed => moveSpeed * skillSpeedMultiplier;
    [SerializeField] Rigidbody2D playerRigidbody;
    [SerializeField] public CharacterData characterData;

    private bool IsGround = false;
    private bool IsCanDoubleJump = false;
    [SerializeField]private GamePlayerControl gamePlayerControl;
   [SerializeField] private GamePlayerAnimationControl gamePlayerAnimationControl;
    private ISkill skill;
    private void Awake()
    {
        
        gamePlayerControl = GetComponent<GamePlayerControl>();
        gamePlayerAnimationControl = GetComponent<GamePlayerAnimationControl>();
    }
    private void Start()
    {
        PlayerStartPosition = transform.position;
        audioSource = GetComponent<AudioSource>();
        skillIcon = GetComponent<SkillIcon>();
    }
    private void Update()
    {
        if (skill != null)
            Cooldown += Time.deltaTime;
        playerRigidbody.velocity += Vector2.down * characterData.gravity * Time.deltaTime;
        
    }

    public void SkillSet(Skills skill)
    {
        switch (skill) {
            case Skills.One:
                this.skill = new OneSkill(skillOneClip);
                break;
            case Skills.Two:
                this.skill = new TwoSkill(skillTwoClip);
                break;
            case Skills.Three:
                this.skill = new ThreeSkill(skillThreeClip);
                break;
            }
    }
    public IEnumerator SpeedBoost(float multiplier, float duration)
    {
       
        skillSpeedMultiplier *= multiplier;
        yield return new WaitForSeconds(duration);
        skillSpeedMultiplier /= multiplier;
        
    }
    public IEnumerator SizeChange(float multiplier, float duration)
    {
        gameObject.transform.localScale = new Vector3(multiplier, multiplier, multiplier);


        yield return new WaitForSeconds(duration);

        gameObject.transform.localScale = Vector3.one;

    }
    public IEnumerator IgnoreWall(float duration)
    {
        if (isIgnoringWall) yield break; 
        isIgnoringWall = true;

        
        Physics2D.IgnoreLayerCollision(
            LayerMask.NameToLayer("Player"),
            LayerMask.NameToLayer("Wall"),
            true
        );

        yield return new WaitForSeconds(duration);

        
        Physics2D.IgnoreLayerCollision(
            LayerMask.NameToLayer("Player"),
            LayerMask.NameToLayer("Wall"),
            false
        );

        isIgnoringWall = false;
    }


    public void PlayerInit(CharacterData data)
    {
        characterData = data;
        Hp = characterData.maxHp;
        moveSpeed = characterData.speed;
        gamePlayerAnimationControl.InitAnimator(characterData.animatorController);
        SkillSet(data.skills);
        
    }
    
    
    public void Jump()
    {
        if (IsGround)
        {
            audioSource.PlayOneShot(jumpClip);
            gamePlayerAnimationControl.JumpAnimation();
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, characterData.jumpForce);
            IsGround = false;
            
        }
        else if (IsCanDoubleJump)
        {
            DoubleJump();
        }

    }
    public void DoubleJump()
    {
        audioSource.PlayOneShot(jumpClip);
            gamePlayerAnimationControl.DoubleJumpEffect();
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, characterData.doubleJumpForce);
            IsCanDoubleJump = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Ground")) {
                IsGround = true;
                IsCanDoubleJump=true;
                gamePlayerAnimationControl.EndJumpAnimation();

        }
    
    }
    private void Damaged()
    {
        audioSource.PlayOneShot(hurtclip);
        Hp -= 1;
        StartCoroutine(IgnoreWall(3f));
        gamePlayerAnimationControl.DamagedAnimation();
        if (Hp <= 0)
        {
            gamePlayerAnimationControl.DieAnimation();
            RunGameManager.Instance.EndGame();
            Debug.Log("die");
            
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            Damaged();
        }
        else if (collision.CompareTag("BackWall")){
            Debug.Log("µÞº®");
            Damaged();
            transform.position = PlayerStartPosition;
        }
    }
    public void UseSkill()
    {
        if (Cooldown < characterData.cooldown) return;
        if(skill.SkillClip != null&&audioSource != null)
        {
            audioSource.PlayOneShot(skill.SkillClip);
        }
        skill.UseSkill(this);
        gamePlayerAnimationControl.UseSkillAnimation();
        skillIcon.StartCooltime(Cooldown);
    }
    public void UseSlide()
    {
        audioSource.PlayOneShot(slideClip);
        gamePlayerAnimationControl.UseSlideAnimation();
    }
    public void EndSlide()
    {
        gamePlayerAnimationControl.EndSlideAnimation();
    }

    public void HpHeal(int heal)
    {
        Hp += heal;
        if (Hp >= characterData.maxHp)
        {
            Hp = characterData.maxHp;
        }
    }
}
