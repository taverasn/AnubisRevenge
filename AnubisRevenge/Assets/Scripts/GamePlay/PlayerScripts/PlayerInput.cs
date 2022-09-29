using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    //Component Variables
    private PlayerAnimationHandler pAnimHandler;
    private PlayerController pCtrl;
    private PlayerTimeManager pTime;

    // State Variables
    private bool isRunning;
    private bool isCrouching;
    private bool isIdle;
    private bool isWalking;
    private bool isJumping;
    private bool isMeleePressed;
    private bool isShootPressed;
    private bool isThrowPressed;
    private bool releasedJump;
    private float xAxis;


    void Start()
    {
        pTime = GetComponent<PlayerTimeManager>();
        pCtrl = GetComponent<PlayerController>();
        pAnimHandler = GetComponent<PlayerAnimationHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        // If Game Over Stop Player Input
        if(!pCtrl.gameOver)
        {
            xAxis = Input.GetAxis("Horizontal");
            // Jump Key Pressed?
            if (Input.GetButtonDown("Jump") && pCtrl.GetisGrounded())
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
            if (Input.GetKeyDown(KeyCode.Mouse0) && pTime.GetTimeBtwMeleeAttack() > pTime.GetStartTimeBtwMeleeAttack() && !isRunning && !isWalking)
            {
                pTime.SetTimeBtwMeleeAttack(0);
                isMeleePressed = true;
            }
            // Shoot Key Pressed?
            if (Input.GetKeyDown(KeyCode.Mouse1) && pTime.GetTimeBtwRangeAttack() > pTime.GetStartTimeBtwRangeAttack())
            {
                pTime.SetTimeBtwRangeAttack(0);
                isShootPressed = true;
            }
            // Throw Key Pressed and not Running and not Walking?
            if (Input.GetKeyDown(KeyCode.F) && pTime.GetTimeBtwThrowAttack() > pTime.GetStartTimeBtwThrowAttack() && !isRunning && !isWalking)
            {
                pTime.SetTimeBtwThrowAttack(0);
                isThrowPressed = true;
            }
        }
    }

    private void FixedUpdate()
    {
        // Player Collider hitting Ground Collider?
        if (pCtrl.GetisGrounded() && !pAnimHandler.isMelee && !pAnimHandler.isShooting && !pAnimHandler.isThrowing)
        {
            // If xAxis is != to 0 check if player is using running input if not set running to true
            if (xAxis != 0)
            {
                isIdle = false;
                if (GetisRunning())
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

    // Getters and Setters
    public bool GetisMeleePressed()
    {
        return isMeleePressed;
    }
    public void SetisMeleePressed(bool _isMeleePressed)
    {
        isMeleePressed = _isMeleePressed;
    }    
    public bool GetisShootPressed()
    {
        return isShootPressed;
    }
    public void SetisShootPressed(bool _isShootPressed)
    {
        isShootPressed = _isShootPressed;
    }    
    public bool GetisThrowPressed()
    {
        return isThrowPressed;
    }
    public void SetisThrowPressed(bool _isThrowPressed)
    {
        isThrowPressed = _isThrowPressed;
    }
    public bool GetisJumping()
    {
        return isJumping;
    }
    public void SetisJumping(bool _isJumping)
    {
        isJumping = _isJumping;
    }
    public bool GetreleasedJump()
    {
        return releasedJump;
    }    
    public void SetreleasedJump(bool _releasedJump)
    {
        releasedJump = _releasedJump;
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
}
