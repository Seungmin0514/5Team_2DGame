using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    public GameActoinManager actoinManager;

    float horizontalMovement;
    //Jump
    public float jumpPower = 1f;
    //GroundCheck
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask groundLayer;
    Animator animator;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        actoinManager = FindObjectOfType<GameActoinManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(actoinManager != null && actoinManager.isAction)
        {
            rb.velocity = new Vector2(0,rb.velocity.y);
            animator.SetFloat("magnitude", 0);
            return;
        }
        rb.velocity = new Vector2(horizontalMovement * moveSpeed, rb.velocity.y);
        FlipSprite(horizontalMovement);
        animator.SetFloat("yVelocity", rb.velocity.y);
        animator.SetFloat("magnitude", rb.velocity.magnitude);
    }
    private void FlipSprite(float moveInput)
    {
        if(moveInput > 0)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }else if(moveInput < 0)
        {
            transform.localScale = new Vector3(-1,transform.localScale.y, transform.localScale.z);
        }
    }
    public void Move(InputAction.CallbackContext context)
    {
        if(actoinManager != null && actoinManager.isAction)
        {
            horizontalMovement = 0;
            return;
        }
        horizontalMovement = context.ReadValue<Vector2>().x;
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if(actoinManager != null && actoinManager.isAction)
        {
            return;
        }
        if (isGrounded())
        {
            if (context.performed)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                animator.SetTrigger("Jump");
            }
            else if (context.canceled)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }
        }  
    }

    private bool isGrounded()
    {
        if(Physics2D.OverlapBox(groundCheckPos.position,groundCheckSize, 0, groundLayer))
        {
            return true;
        }
        return false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawCube(groundCheckPos.position,groundCheckSize);

    }
}
