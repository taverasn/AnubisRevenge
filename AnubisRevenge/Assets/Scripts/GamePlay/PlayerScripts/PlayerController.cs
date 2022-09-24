using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float xAxis;
    private float yAxis;

    private PlayerInput pInput;


    public float horizontalSpeed = 10;
    public float horizontalSprintSpeed = 20;
    private bool isGrounded;
    public bool facingRight = true;
    public bool gameOver;
    private Rigidbody2D rb;
    private PlayerAttack pAttack;



    // Jump Variables
    public float jumpVelocity = 850;
    [SerializeField] private float jumpTimeCounter;
    [SerializeField] private float jumpTime;
    [SerializeField] private bool isJumping;
    private bool startTimer;
    private bool releasedJump;
    private float gravityScale = 4f;

    // Start is called before the first frame update
    private void Start()
    {
        pInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        pAttack = GetComponent<PlayerAttack>();
        jumpTimeCounter = jumpTime;
    }

    public bool GetisGrounded()
    {
        return isGrounded;
    }




    private void Update()
    {
        if(!gameOver)
        {
            // Checking for inputs
            xAxis = Input.GetAxis("Horizontal");
            yAxis = Input.GetAxis("Vertical");
            
            // Jump Key Pressed?
            if(Input.GetButtonDown("Jump"))
            {
                isJumping = true;
                isGrounded = false;
            }

            if (Input.GetButtonUp("Jump"))
            {
                releasedJump = true;
            }

            if (startTimer)
            {
                jumpTimeCounter -= Time.deltaTime;
                if (jumpTimeCounter <= 0)
                {
                    releasedJump = true;
                }
            }


        }

    }
    private void FixedUpdate()
    {
        if (!gameOver)
        {
            Vector2 vel = new Vector2(0, rb.velocity.y);
            if(!pInput.GetisCrouching())
            {

                // Check movement update based on input

                if (xAxis < 0)
                {
                    Debug.Log("negative x");
                    if(pInput.GetisRunning())
                    {
                        Debug.Log("neg sprint");
                        vel.x = -horizontalSprintSpeed;
                    }
                    else
                    {
                        vel.x = -horizontalSpeed;
                    }
                    transform.localScale = new Vector2(-.5f, .5f);
                    Debug.Log("turn around");
                }
                else if (xAxis > 0)
                {
                    if (pInput.GetisRunning())
                    {
                        vel.x = horizontalSprintSpeed;
                    }
                    else
                    {
                        vel.x = horizontalSpeed;
                    }
                    transform.localScale = new Vector2(.5f, .5f);
            
                }
                else
                {
                    vel.x = 0;
                }


            }

            // Check if trying to jump
            if (isJumping && isGrounded == false && !pAttack.isMelee && !pAttack.isShooting && !pAttack.isThrowing)
            {
                rb.gravityScale = 1;
                rb.AddForce(new Vector2(0, jumpVelocity));
                isJumping = false;
                startTimer = true;
            }
            
            // Checking for release of jump key and resetting jump timer
            if(releasedJump)
            {
                rb.gravityScale = gravityScale;
                releasedJump = false;
                jumpTimeCounter = jumpTime;
                startTimer = false;
            }



            rb.velocity = vel;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }
}
