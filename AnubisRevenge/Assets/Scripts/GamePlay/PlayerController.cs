using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private string currentState;
    private float xAxis;
    public float yAxis;
    private bool isRunning;
    private bool isCrouching;
    private bool isIdle;
    private bool isWalking;
    public float horizontalSpeed = 10;
    public float horizontalSprintSpeed = 20;
    [SerializeField] private bool isGrounded;
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

    //Animation States
    private const string PLAYER_IDLE = "Player_Idle";
    private const string PLAYER_RUN = "Player_Run";
    private const string PLAYER_WALK = "Player_Walk";
    private const string PLAYER_JUMP = "Player_Jump";
    private const string PLAYER_CROUCH = "Player_Crouch";

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        pAttack = GetComponent<PlayerAttack>();
        jumpTimeCounter = jumpTime;
    }

    public bool GetisGrounded()
    {
        return isGrounded;
    }

    public bool GetisCrouching()
    {
        return isCrouching;
    }    
    public bool GetisRunning()
    {
        return isRunning;
    }
    public bool GetisIdle()
    {
        return isIdle;
    }
    public bool GetisWalking()
    {
        return isWalking;
    }
    public void ChangeAnimationState(string newState)
    {
        // stop same animation from interrupting itself
        if (currentState == newState) return;

        // play the animation
        animator.Play(newState);

        // reassign the current state
        currentState = newState;
    }
    // Update is called once per frame



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

            // Sprint Key Pressed
            if (Input.GetButtonDown("Sprint"))
            {
                isRunning = true;
            }

            // Sprint Key Released
            if (Input.GetButtonUp("Sprint"))
            {
                isRunning = false;
            }
            // Crouch Key Pressed?
            if (Input.GetButtonDown("Crouch"))
            {
                isCrouching = true;
            }
            if(Input.GetButtonUp("Crouch"))
            {
                isCrouching = false;
            }
        }

    }
    private void FixedUpdate()
    {
        //TODO: Walk Shoot
        // Track Walk
        if (!gameOver)
        {
            Vector2 vel = new Vector2(0, rb.velocity.y);
            if(!isCrouching)
            {

                // Check movement update based on input

                if (xAxis < 0)
                {
                    if(isRunning == true)
                    {
                        vel.x = -horizontalSprintSpeed;
                    }
                    else
                    {
                        vel.x = -horizontalSpeed;
                    }
                    transform.localScale = new Vector2(-.5f, .5f);
                }
                else if (xAxis > 0)
                {
                    if (isRunning == true)
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

                if(isGrounded && !pAttack.isMelee && !pAttack.isShooting && !pAttack.isThrowing)
                {
                    if(xAxis != 0)
                    {
                        isIdle = false;
                        if(isRunning == true)
                        {
                            isWalking = false;
                            ChangeAnimationState(PLAYER_RUN);
                        } else
                        {
                            isWalking = true;
                            ChangeAnimationState(PLAYER_WALK);
                        }
                    }
                    else
                    {
                        isWalking = false;
                        isIdle = true;
                        ChangeAnimationState(PLAYER_IDLE);
                    }
                }
            }

            // Check if trying to jump
            if (isJumping && isGrounded == false && !pAttack.isMelee && !pAttack.isShooting && !pAttack.isThrowing)
            {
                rb.gravityScale = 1;
                rb.AddForce(new Vector2(0, jumpVelocity));
                ChangeAnimationState(PLAYER_JUMP);
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

            // Checking if Crouch Key is being pressed before animation
            if(isGrounded && isCrouching && !pAttack.isMelee && !pAttack.isShooting && !pAttack.isThrowing)
            {
                ChangeAnimationState(PLAYER_CROUCH);
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
