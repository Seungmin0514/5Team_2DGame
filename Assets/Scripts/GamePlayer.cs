using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GamePlayer : MonoBehaviour
{

    private int Hp;
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
    private void Update()
    {
        
            playerRigidbody.velocity += Vector2.down * characterData.gravity * Time.deltaTime;
        
    }

    


    public void PlayerInit(CharacterData data)
    {
        characterData = data;
        Hp = characterData.maxHp;
        gamePlayerAnimationControl.InitAnimator(characterData.animatorController);
    }
    
    
    public void Jump()
    {
        if (IsGround)
        {
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
    private void HpCheck()
    {
        if (Hp <= 0)
        {
            gamePlayerAnimationControl.DieAnimation();
            Debug.Log("die");
            
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            Hp -= 1;
            gamePlayerAnimationControl.DamagedAnimation();
            HpCheck();

        }
    }
    public void UseSkill()
    {
        gamePlayerAnimationControl.UseSkillAnimation();
    }
    public void UseSlide()
    {
        gamePlayerAnimationControl.UseSlideAnimation();
    }
    public void EndSlide()
    {
        gamePlayerAnimationControl.EndSlideAnimation();
    }

}
