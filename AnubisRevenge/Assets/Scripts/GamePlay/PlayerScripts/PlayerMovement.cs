using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Component Variables
    private PlayerController pCtrl;

    // Player X Movement Variables
    [SerializeField] private float verticalClimbSpeed;
    [SerializeField] internal float horizontalClimbSpeed;
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float horizontalSprintSpeed;
    internal bool facingRight = true;

    // Jump Variables
    [SerializeField] private float jumpVelocity;
    private float jumpTimeCounter;
    [SerializeField] private float jumpTime;
    private bool startTimer;
    [SerializeField] internal float gravityScale;
    [SerializeField] internal float fallMultiplier;

    internal bool isMoving = true;
    // Start is called before the first frame update
    private void Start()
    {
        pCtrl = GetComponent<PlayerController>();
        jumpTimeCounter = jumpTime;
    }
    private void Update()
    {
        // If game over stop player movement
        if(!pCtrl.gameOver)
        {
            // Checking for inputs
            // Force Releases jump button if user is holding it for to long
            if (startTimer)
            {
                jumpTimeCounter -= Time.deltaTime;
                if (jumpTimeCounter <= 0)
                {
                    pCtrl.pInput.releasedJump = true;
                    pCtrl.pInput.coyoteTimeCounter = 0f;
                }
            }
        }
    }
    private void FixedUpdate()
    {
        // If game over stop player movement
        if (!pCtrl.gameOver)
        {
            //Vector2 vel = new Vector2(0, pCtrl.rb.velocity.y);
            // Crouching not pressed?
            if(!pCtrl.pInput.isCrouching && isMoving)
            {
                // Check movement update based on input
                // Player Moving in +X or -X direction
                if (pCtrl.xAxis < 0)
                {
                    facingRight = false;
                    // Change between running and walking speed depending if the run button input is true
                    if(pCtrl.pInput.isClimbing)
                    {
                        pCtrl.rb.velocity = new Vector2(-horizontalClimbSpeed, pCtrl.rb.velocity.y);
                    }
                    else if(pCtrl.pInput.isRunning)
                    {
                        pCtrl.rb.velocity = new Vector2(-horizontalSprintSpeed, pCtrl.rb.velocity.y);
                    }
                    else
                    {
                        pCtrl.rb.velocity = new Vector2(-horizontalSpeed, pCtrl.rb.velocity.y);

                    }
                    // flips player to the left
                    transform.localScale = new Vector2(-.5f, .5f);
                }
                else if (pCtrl.xAxis > 0)
                {
                    facingRight = true;
                    // Change between running and walking speed depending if the run button input is true
                    if (pCtrl.pInput.isClimbing)
                    {
                        pCtrl.rb.velocity = new Vector2(horizontalClimbSpeed, pCtrl.rb.velocity.y);
                    }
                    else if (pCtrl.pInput.isRunning)
                    {
                        pCtrl.rb.velocity = new Vector2(horizontalSprintSpeed, pCtrl.rb.velocity.y);
                    }
                    else
                    {
                        pCtrl.rb.velocity = new Vector2(horizontalSpeed, pCtrl.rb.velocity.y);
                    }
                    // flips player to the right
                    transform.localScale = new Vector2(.5f, .5f);
                }
                else
                {
                    pCtrl.rb.velocity = new Vector2(0, pCtrl.rb.velocity.y);
                }
            }
            if(pCtrl.pInput.isClimbing)
            {
                if (pCtrl.yAxis > 0)
                {
                    pCtrl.rb.velocity = new Vector2(pCtrl.rb.velocity.x, verticalClimbSpeed);
                }
                else if(pCtrl.yAxis < 0)
                {
                    pCtrl.rb.velocity = new Vector2(pCtrl.rb.velocity.x, -verticalClimbSpeed);
                }
                else if (pCtrl.pInput.isClimbing && pCtrl.yAxis == 0 && pCtrl.xAxis == 0)
                {
                    pCtrl.rb.gravityScale = 0;
                    pCtrl.rb.velocity = Vector2.zero;
                }
            }            
            else if(!pCtrl.pInput.isClimbing && pCtrl.rb.gravityScale == 0)
            {
                pCtrl.rb.gravityScale = gravityScale;
            }
            // Check if trying to jump
            if (pCtrl.pInput.isJumping)
            {
                pCtrl.rb.velocity = new Vector2(pCtrl.rb.velocity.x, jumpVelocity);
                startTimer = true;
            }

            // Check if trying to jump
            if (pCtrl.pInput.isJumping)
            {
                pCtrl.rb.velocity = new Vector2(pCtrl.rb.velocity.x, jumpVelocity);
                startTimer = true;
            }

            // Checking for release of jump key and resetting jump timer
            if (pCtrl.rb.velocity.y > 0 && pCtrl.pInput.releasedJump)
            {
                pCtrl.rb.velocity = new Vector2(pCtrl.rb.velocity.x, pCtrl.rb.velocity.y * 0.5f);
                pCtrl.pInput.isJumping = false;
                pCtrl.pInput.releasedJump = false;
                jumpTimeCounter = jumpTime;
                startTimer = false;
            }
            if (pCtrl.rb.velocity.y < 0)
            {
                pCtrl.rb.velocity = new Vector2(pCtrl.rb.velocity.x, pCtrl.rb.velocity.y * fallMultiplier);
            }
        }
    }
}
