using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GamePlayer : MonoBehaviour
{

    private int playerHP;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float doublejumpForce;
    [SerializeField] float gravity = 100f;
    [SerializeField] Rigidbody2D rigidbody;
    [SerializeField] private CharacterData characterData;

    private bool IsGround = false;
    private bool IsCanDoubleJump = false;
    private GamePlayerControl gamePlayerControl;
    private GamePlayerAnimationControl gamePlayerAnimationControl;
    private ISkill skill;
    private void Awake()
    {
        PlayerInit();
        gamePlayerControl = GetComponent<GamePlayerControl>();
        gamePlayerAnimationControl = GetComponent<GamePlayerAnimationControl>();
    }
    private void Update()
    {
        rigidbody.velocity += Vector2.down * gravity * Time.deltaTime;


        
    }



    public void PlayerInit(int PlayerHP = 3,float speed = 10f, float jumpForce = 30f, float doubleJumpForce = 20f)
    {
        this.playerHP = PlayerHP;
        this.speed = speed;
        this.jumpForce = jumpForce;
        this.doublejumpForce = doubleJumpForce;
        
    }
    
    
    public void Jump()
    {
        if (IsGround)
        {
            gamePlayerAnimationControl.JumpAnimation();
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
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
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, doublejumpForce);
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
        if (playerHP <= 0)
        {
            gamePlayerAnimationControl.DieAnimation();
            Debug.Log("die");
            
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            playerHP -= 1;
            gamePlayerAnimationControl.DamagedAnimation();
            HpCheck();

        }
    }
    public void UseSkill()
    {
        gamePlayerAnimationControl.UseSkillAnimation();
    }



}
