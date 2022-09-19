using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public float horizontalSpeed = 10;
    public float horizontalSprintSpeed = 200;
    public float verticalSpeed = 10;
    public bool isGrounded = true;
    public bool doublejump;
    public bool facingRight = true;
    public bool gameOver;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    [Range (1, 10)] public float jumpVelocity;


    private Rigidbody2D rb;
    private Health playerHealth;
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerHealth = gameObject.GetComponent<Health>();
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
            //TODO: Walk Shoot
            // Track Walk
        if(!gameOver)
        {
            float movement = Input.GetAxis("Horizontal");
            if(animator.GetBool("isCrouching") == false && animator.GetBool("isClimbing") == false)
            {
                if (animator.GetBool("Sprinting") == false)
                {
                    transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * horizontalSpeed;
                }
                else if (animator.GetBool("Sprinting") == true)
                {
                    transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * horizontalSprintSpeed;

                }
            }
            if(movement != 0 && isGrounded == true)
            {
                animator.SetBool("isMoving", true);
            } else
            {
                animator.SetBool("isMoving", false);
            }
            if(movement < 0 && facingRight)
            {
                Flip();
            }
            if (movement > 0 && !facingRight)
            {
                Flip();
            }
        }
    }

    private void Update()
    {
        if(!gameOver)
        {

            // TODO: Run Shoot
            //Sprint Key
            if(isGrounded && Input.GetButton("Sprint"))
            {
                animator.SetBool("Sprinting", true);
            }
            if(isGrounded && !Input.GetButton("Sprint") && animator.GetBool("Sprinting"))
            {
                animator.SetBool("Sprinting", false);
            }

            if(!animator.GetBool("isCrouching"))
            {
                // Check for double jump
                if (isGrounded && !Input.GetButton("Jump"))
                {
                    doublejump = false;
                }

                // TODO: Create jump animation triggers
                // Jump
                if (Input.GetButtonDown("Jump"))
                {
                    if(isGrounded || doublejump)
                    {
                        rb.velocity = Vector2.up * jumpVelocity;
                        if(rb.velocity.y < 0)
                        {
                            rb.velocity += Vector2.up * 9.81f * (fallMultiplier - 1) * Time.deltaTime;
                        }
                        isGrounded = false;
                        animator.SetTrigger("Jump");
                
                        doublejump = !doublejump;
                    }
                }
            }
            
            // TODO: Create Crouch animations triggers
            // Crouch
            if(isGrounded && Input.GetButton("Crouch"))
            {
                animator.SetBool(("isCrouching"), true);
            }
            if(isGrounded && !Input.GetButton("Crouch") && animator.GetBool("isCrouching"))
            {
                animator.SetBool(("isCrouching"), false);
            }
        }

    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }

/*        if(other.gameObject.tag == "Wall")
        {
            animator.SetBool("isClimbing", true);
        }*/
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
}
