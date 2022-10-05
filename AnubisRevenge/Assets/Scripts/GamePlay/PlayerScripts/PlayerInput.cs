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
    internal bool releasedJump = true;
    [SerializeField] internal bool isClimbing;

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
            if (!pCtrl.pAnimHandler.isMelee && !pCtrl.pAnimHandler.isShooting && !pCtrl.pAnimHandler.isThrowing)
            {
                // Jump Key Pressed?
                if (Input.GetButtonDown("Jump") && (pCtrl.pColl.isGrounded() || pCtrl.pColl.isClimbing()))
                {
                    isJumping = true;
                    isClimbing = false;
                }
                // Jump Key Released?
                if (Input.GetButtonUp("Jump"))
                {
                    releasedJump = true;
                }
            }
            // Sprint Key Pressed?
            if (Input.GetKeyDown(KeyCode.LeftShift) && pCtrl.pColl.isGrounded())
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
                if (Input.GetKeyDown(KeyCode.Mouse0) && pCtrl.pTime.timeBtwMeleeAttack > pCtrl.pTime.startTimeBtwMeleeAttack && !pCtrl.pAnimHandler.isShooting && !pCtrl.pAnimHandler.isThrowing)
                {
                    pCtrl.pTime.timeBtwMeleeAttack = 0;
                    isMeleePressed = true;
                }
                // Throw Key Pressed and not Running and not Walking?
                if (Input.GetKeyDown(KeyCode.F) && pCtrl.pTime.timeBtwThrowAttack > pCtrl.pTime.startTimeBtwThrowAttack && !pCtrl.pAnimHandler.isShooting && !pCtrl.pAnimHandler.isMelee && PlayerPrefs.GetInt("dynamite") > 0)
                {
                    pCtrl.pTime.timeBtwThrowAttack = 0;
                    isThrowPressed = true;
                }
            }
            // Player Collider hitting Ground Collider?
            if (pCtrl.pColl.isGrounded() && !pCtrl.pAnimHandler.isMelee && !pCtrl.pAnimHandler.isShooting && !pCtrl.pAnimHandler.isThrowing)
            {
                // If xAxis is != to 0 check if player is using running input if not set running to true
                if (pCtrl.xAxis != 0)
                {
                    isIdle = false;
                    if(pCtrl.pColl.isGrounded())
                    {
                        if (isRunning)
                        {
                            isWalking = false;
                        }
                        else
                        {
                            isWalking = true;
                        }
                    }
                }
                // If xAxis is = 0 set Idle to true
                else
                {
                    isWalking = false;
                    isIdle = true;
                }
            }
            if(pCtrl.pColl.isClimbing() && pCtrl.yAxis != 0 && !Input.GetButtonDown("Jump"))
            {
                isClimbing = true;
            }
            else if (!pCtrl.pColl.isClimbing())
            {
                isClimbing = false;
            }
        }
    }
}
