using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Component Variables
    private PlayerController pCtrl;

    // Player X Movement Variables
    private float xAxis;
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float horizontalSprintSpeed;
    internal bool facingRight = true;

    // Jump Variables
    [SerializeField] private float jumpVelocity;
    private float jumpTimeCounter;
    [SerializeField] private float jumpTime;
    private bool startTimer;
    [SerializeField] private float gravityScale;

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
            xAxis = Input.GetAxis("Horizontal") * .1f;
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
        if (!pCtrl.gameOver)
        {
            Vector2 vel = new Vector2(0, pCtrl.rb.velocity.y);
            // Crouching not pressed?
            if(!pCtrl.pInput.isCrouching && !pCtrl.pInput.isClimbing)
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
            if (pCtrl.pInput.isJumping && !pCtrl.pAnimHandler.isMelee && !pCtrl.pAnimHandler.isShooting && !pCtrl.pAnimHandler.isThrowing)
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
}
