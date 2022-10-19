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
    bool startTimer;
    internal bool releasedJump = true;
    internal bool isClimbing;
    [SerializeField] private float coyoteTime = 0.2f;
    internal float coyoteTimeCounter;
    [SerializeField] private float jumpBufferTime = 0.2f;
    internal float jumpBufferCounter;
    public float throwMultiplierTimer;

    void Start()
    {
        pCtrl = GetComponent<PlayerController>();
    }

    void Update()
    {
        // If Game Over Stop Player Input
        if(!pCtrl.gameOver)
        {
            if(!pCtrl.pAnimHandler.takingDamage)
            {

                if(Input.GetKeyDown(KeyCode.F))
                {
                    startTimer = true;
                }
                if(startTimer)
                {
                    throwMultiplierTimer += Time.deltaTime * 3;
                    if (throwMultiplierTimer >= 1 || Input.GetKeyUp(KeyCode.F))
                    {
                        startTimer = false;
                    }
                }

                if (pCtrl.pColl.isGrounded())
                    coyoteTimeCounter = coyoteTime;
                else
                    coyoteTimeCounter -= Time.deltaTime;
                if (Input.GetButtonDown("Jump"))
                    jumpBufferCounter = jumpBufferTime;
                else
                    jumpBufferCounter -= Time.deltaTime;
                // Jump Key Pressed?
                if (jumpBufferCounter > 0f && (coyoteTimeCounter > 0f || pCtrl.pColl.isClimbing()))
                {
                    gameManager.instance.soundManager.aud.PlayOneShot(gameManager.instance.soundManager.jump);
                    jumpBufferCounter = 0f;
                    isJumping = true;
                    isClimbing = false;
                }
                // Jump Key Released?
                if (Input.GetButtonUp("Jump"))
                {
                    releasedJump = true;
                    coyoteTimeCounter = 0f;
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
                if(!isClimbing)
                {
                    // Shoot Key Pressed?
                    if (Input.GetKeyDown(KeyCode.Mouse1) && pCtrl.pTime.timeBtwRangeAttack > pCtrl.pTime.startTimeBtwRangeAttack && !pCtrl.pAnimHandler.isMelee && !pCtrl.pAnimHandler.isThrowing)
                    {
                        pCtrl.pTime.timeBtwRangeAttack = 0;
                        isShootPressed = true;
                    }
                    // Melee Key Pressed and not Running and not Walking?
                    if (Input.GetKeyDown(KeyCode.Mouse0) && pCtrl.pTime.timeBtwMeleeAttack > pCtrl.pTime.startTimeBtwMeleeAttack && !pCtrl.pAnimHandler.isShooting && !pCtrl.pAnimHandler.isThrowing && (isIdle || !pCtrl.pColl.isGrounded()))
                    {
                        pCtrl.pTime.timeBtwMeleeAttack = 0;
                        isMeleePressed = true;
                    }
                    // Throw Key Pressed and not Running and not Walking?
                    if (Input.GetKeyUp(KeyCode.F) && pCtrl.pTime.timeBtwThrowAttack > pCtrl.pTime.startTimeBtwThrowAttack && !pCtrl.pAnimHandler.isShooting && !pCtrl.pAnimHandler.isMelee && (isIdle || !pCtrl.pColl.isGrounded()) && PlayerPrefs.GetInt("dynamite") > 0)
                    {
                        pCtrl.pTime.timeBtwThrowAttack = 0;
                        isThrowPressed = true;
                    }
                }
                // If xAxis is != to 0 check if player is using running input if not set running to true
                if (pCtrl.xAxis != 0)
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
                if(pCtrl.pColl.isClimbing() && pCtrl.yAxis != 0 && !isJumping)
                {
                    isClimbing = true;
                }
                else if (!pCtrl.pColl.isClimbing() || isJumping)
                {
                    isClimbing = false;
                }
            }
        }
    }
}
