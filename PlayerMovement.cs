using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float jumpForce = 400f;
    public float doubleJumpForce = 300f;
    public float moveSpeed = 10f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask whatIsGround;

    private Rigidbody2D rb;
    private Animator anim;
    private bool isGrounded;
    private bool canDoubleJump;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        //flip the player while moving left
        if(horizontalInput > 0.01f)
        {
            transform.localScale = new Vector3(0.35f,0.35f,1);
        }
        else if(horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-0.35f,0.35f,1);
        }

        if (isGrounded)
        {
            canDoubleJump = true;
        }
        anim.SetBool("mimbo_run", horizontalInput != 0);
        anim.SetBool("mimbo_grounded", isGrounded);
    }

    void Update()
    {
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(new Vector2(0f, jumpForce));
                anim.SetTrigger("mimbo_jump");
            }
        }
        else
        {
            if (canDoubleJump && Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                rb.AddForce(new Vector2(0f, doubleJumpForce));
                canDoubleJump = false;
            }
        }
    }
}