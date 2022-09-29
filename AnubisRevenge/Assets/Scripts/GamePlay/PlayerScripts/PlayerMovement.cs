using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Component Variables
    private PlayerController pCtrl;

    // Player X Movement Variables
    private float xAxis;
    public float horizontalSpeed = 10;
    public float horizontalSprintSpeed = 20;
    internal bool facingRight;

    // State Variables
    private bool isGrounded;
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
        pCtrl = GetComponent<PlayerController>();
        jumpTimeCounter = jumpTime;
    }

    // Getter
    public bool GetisGrounded()
    {
        return isGrounded;
    }
    private void Update()
    {
        // If game over stop player movement
        if(!gameOver)
        {
            // Checking for inputs
            xAxis = Input.GetAxis("Horizontal");
            // Check to see is player is on the floor
            if(pCtrl.pInput.isJumping)
            {
                isGrounded = false;
            }
            // Force Releases jump button if user is holding it for to long
            if (startTimer)
            {
                jumpTimeCounter -= Time.deltaTime;
                if (jumpTimeCounter <= 0)
                {
                    pCtrl.pInput.releasedJump = true;
                }
            }
        }
    }
    private void FixedUpdate()
    {
        // If game over stop player movement
        if (!gameOver)
        {
            Vector2 vel = new Vector2(0, pCtrl.rb.velocity.y);
            // Crouching not pressed?
            if(!pCtrl.pInput.isCrouching)
            {
                // Check movement update based on input
                // Player Moving in +X or -X direction
                if (xAxis < 0)
                {
                    facingRight = false;
                    // Change between running and walking speed depending if the run button input is true
                    if(pCtrl.pInput.isRunning)
                    {
                        vel.x = -horizontalSprintSpeed;
                    }
                    else
                    {
                        vel.x = -horizontalSpeed;
                    }
                    // flips player to the left
                    transform.localScale = new Vector2(-.5f, .5f);
                }
                else if (xAxis > 0)
                {
                    facingRight = true;
                    // Change between running and walking speed depending if the run button input is true
                    if (pCtrl.pInput.isRunning)
                    {
                        vel.x = horizontalSprintSpeed;
                    }
                    else
                    {
                        vel.x = horizontalSpeed;
                    }
                    // flips player to the right
                    transform.localScale = new Vector2(.5f, .5f);
                }
                else
                {
                    vel.x = 0;
                }
            }
            // Check if trying to jump
            if (pCtrl.pInput.isJumping && !isGrounded && !pCtrl.pAnimHandler.isMelee && !pCtrl.pAnimHandler.isShooting && !pCtrl.pAnimHandler.isThrowing)
            {
                pCtrl.rb.gravityScale = 1;
                pCtrl.rb.AddForce(new Vector2(0, jumpVelocity));
                pCtrl.pInput.isJumping = false;
                startTimer = true;
            }
            // Checking for release of jump key and resetting jump timer
            if(pCtrl.pInput.releasedJump)
            {
                pCtrl.rb.gravityScale = gravityScale;
                pCtrl.pInput.releasedJump = false;
                jumpTimeCounter = jumpTime;
                startTimer = false;
            }
            pCtrl.rb.velocity = vel;
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
