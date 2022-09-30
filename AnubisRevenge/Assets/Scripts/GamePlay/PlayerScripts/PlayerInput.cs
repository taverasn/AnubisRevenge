using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    //Component Variables
    private PlayerController pCtrl;

    // State Variables
    internal bool isRunning;
    internal bool isCrouching;
    internal bool isIdle;
    internal bool isWalking;
    internal bool isJumping;
    internal bool isMeleePressed;
    internal bool isShootPressed;
    internal bool isThrowPressed;
    internal bool releasedJump;
    private float xAxis;

    void Start()
    {
        pCtrl = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // If Game Over Stop Player Input
        if(!pCtrl.gameOver)
        {
            xAxis = Input.GetAxis("Horizontal") * .1f;
            // Jump Key Pressed?
            if (Input.GetButtonDown("Jump") && pCtrl.pColl.isGrounded)
            {
                isJumping = true;
            }
            // Jump Key Released?
            if (Input.GetButtonUp("Jump"))
            {
                releasedJump = true;
            }
            // Sprint Key Pressed?
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                isRunning = true;
            }
            // Sprint Key Released?
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                isRunning = false;
            }
            // Crouch Key Pressed?
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                isCrouching = true;
            }
            // Crouch Key Released?
            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                isCrouching = false;
            }

            // Melee Key Pressed and not Running and not Walking?
            if (Input.GetKeyDown(KeyCode.Mouse0) && pCtrl.pTime.timeBtwMeleeAttack > pCtrl.pTime.startTimeBtwMeleeAttack && !isRunning && !isWalking)
            {
                pCtrl.pTime.timeBtwMeleeAttack = 0;
                isMeleePressed = true;
            }
            // Shoot Key Pressed?
            if (Input.GetKeyDown(KeyCode.Mouse1) && pCtrl.pTime.timeBtwRangeAttack > pCtrl.pTime.startTimeBtwRangeAttack)
            {
                pCtrl.pTime.timeBtwRangeAttack = 0;
                isShootPressed = true;
            }
            // Throw Key Pressed and not Running and not Walking?
            if (Input.GetKeyDown(KeyCode.F) && pCtrl.pTime.timeBtwThrowAttack > pCtrl.pTime.startTimeBtwThrowAttack && !isRunning && !isWalking)
            {
                pCtrl.pTime.timeBtwThrowAttack = 0;
                isThrowPressed = true;
            }
        }
    }

    private void FixedUpdate()
    {
        // Player Collider hitting Ground Collider?
        if (pCtrl.pColl.isGrounded && !pCtrl.pAnimHandler.isMelee && !pCtrl.pAnimHandler.isShooting && !pCtrl.pAnimHandler.isThrowing)
        {
            // If xAxis is != to 0 check if player is using running input if not set running to true
            if (xAxis != 0)
            {
                isIdle = false;
                if (isRunning)
                {
                    isWalking = false;
                }
                else
                {
                    isWalking = true;
                }
            }
            // If xAxis is = 0 set Idle to true
            else
            {
                isWalking = false;
                isIdle = true;
            }
        }
    }
}
