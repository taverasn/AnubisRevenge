using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float xAxis;
    private Rigidbody2D rb;
    private PlayerAnimationHandler pAnimHandler;
    private PlayerInput pInput;
    public float horizontalSpeed = 10;
    public float horizontalSprintSpeed = 20;
    private bool isGrounded;
    public bool facingRight = true;
    public bool gameOver;


    // Jump Variables
    public float jumpVelocity = 850;
    [SerializeField] private float jumpTimeCounter;
    [SerializeField] private float jumpTime;
    private bool startTimer;
    private float gravityScale = 4f;

    // Start is called before the first frame update
    private void Start()
    {
        pAnimHandler = GetComponent<PlayerAnimationHandler>();
        pInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
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
            if(pInput.GetisJumping())
            {
                isGrounded = false;
            }
            if (startTimer)
            {
                jumpTimeCounter -= Time.deltaTime;
                if (jumpTimeCounter <= 0)
                {
                    pInput.SetreleasedJump(true);
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
                    if(pInput.GetisRunning())
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
            if (pInput.GetisJumping() && !isGrounded && !pAnimHandler.isMelee && !pAnimHandler.isShooting && !pAnimHandler.isThrowing)
            {
                rb.gravityScale = 1;
                rb.AddForce(new Vector2(0, jumpVelocity));
                pInput.SetisJumping(false);
                startTimer = true;
            }
            // Checking for release of jump key and resetting jump timer
            if(pInput.GetreleasedJump())
            {
                rb.gravityScale = gravityScale;
                pInput.SetreleasedJump(false);
                jumpTimeCounter = jumpTime;
                startTimer = false;
            }
            rb.velocity = vel;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }
}
