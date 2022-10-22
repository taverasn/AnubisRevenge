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
    internal bool isMeleePressed;
    internal bool isAttackPressed;
    internal bool isShootPressed;
    internal bool isThrowPressed;
    bool startTimer;
    internal bool isClimbing;
    internal float throwMultiplierTimer;

    void Start()
    {
        pCtrl = GetComponent<PlayerController>();
    }

    void Update()
    {
        // If Game Over Stop Player Input
        if (!pCtrl.gameOver && !pCtrl.pAnimHandler.takingDamage)
        {
            HorizontalMovementInput();
            ClimbInput();
            if (!isClimbing && !pCtrl.pAnimHandler.isAttacking)
            {
                AttackInput();
            }
        }
    }

    void ClimbInput()
    {
        if (pCtrl.pColl.isClimbing() && pCtrl.yAxis != 0 && !pCtrl.pMove.isJumping)
        {
            isClimbing = true;
        }
        else if (!pCtrl.pColl.isClimbing() || pCtrl.pMove.isJumping)
        {
            isClimbing = false;
        }
    }

    void HorizontalMovementInput()
    {
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
    }

    void AttackInput()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            startTimer = true;
        }
        if (startTimer)
        {
            throwMultiplierTimer += Time.deltaTime * 3;
            if (throwMultiplierTimer >= 1 || Input.GetKeyUp(KeyCode.F))
            {
                startTimer = false;
            }
        }
        // Shoot Key Pressed?
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            isAttackPressed = true;
            isShootPressed = true;
        }
        // Melee Key Pressed and not Running and not Walking?
        if (Input.GetKeyDown(KeyCode.Mouse0) && (isIdle || !pCtrl.pColl.isGrounded()))
        {
            isAttackPressed = true;
            isMeleePressed = true;
        }
        // Throw Key Pressed and not Running and not Walking?
        if (Input.GetKeyUp(KeyCode.F) && (isIdle || !pCtrl.pColl.isGrounded()) && PlayerPrefs.GetInt("dynamite") > 0)
        {
            isAttackPressed = true;
            isThrowPressed = true;
        }
    }
}
