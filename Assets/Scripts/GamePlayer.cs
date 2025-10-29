using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayer : MonoBehaviour
{
    [SerializeField]protected float speed;
    [SerializeField]public float Speed {  get; private set; }

    [SerializeField] protected float jumpForce;
    [SerializeField] public float JumpForce { get; private set; }

    [SerializeField] float gravity = 100f;

    [SerializeField] Rigidbody2D rigidbody;
    

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
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);

    }
}
