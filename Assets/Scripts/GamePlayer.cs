using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GamePlayer : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected float jumpForce;
    [SerializeField] protected float doublejumpForce;
    [SerializeField] float gravity = 100f;
    [SerializeField] Rigidbody2D rigidbody;

    private bool IsGround = false;
    private bool IsCanDoubleJump = false;


    private void Update()
    {
        rigidbody.velocity += Vector2.down * gravity * Time.deltaTime;


        
    }



    public void PlayerInit(float speed = 10f, float jumpForce = 30f)
    {
        this.speed = speed;
        this.jumpForce = jumpForce;
    }
    
    
    public void Jump()
    {
        if (IsGround)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
            IsGround = false;
        }
        else if (IsCanDoubleJump)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, doublejumpForce);
            IsCanDoubleJump = false;
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) {
                IsGround = true;
                IsCanDoubleJump=true;
        }
    
    }


}
