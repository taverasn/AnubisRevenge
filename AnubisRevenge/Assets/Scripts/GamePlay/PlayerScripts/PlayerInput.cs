using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private bool isRunning;
    [SerializeField] private bool isCrouching;
    [SerializeField] private bool isIdle;
    [SerializeField] private bool isWalking;
    [SerializeField] private bool isJumping;
    private PlayerAnimationHandler pAnimHandler;
    private bool isMeleePressed;
    private bool isShootPressed;
    private bool isThrowPressed;

    private bool releasedJump;

    private float xAxis;
    private float yAxis;

    private PlayerController pCtrl;
    private PlayerAttack pAttack;
    // Start is called before the first frame update
    void Start()
    {
        pCtrl = GetComponent<PlayerController>();
        pAttack = GetComponent<PlayerAttack>();
        pAnimHandler = GetComponent<PlayerAnimationHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        xAxis = Input.GetAxis("Horizontal");
        yAxis = Input.GetAxis("Vertical");
        // Jump Key Pressed?
        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
        }
        // Jump Key Released?
        if (Input.GetButtonUp("Jump"))
        {
            releasedJump = true;
        }
        // Sprint Key Pressed?
        if (Input.GetButtonDown("Sprint"))
        {
            isRunning = true;
        }
        // Sprint Key Released?
        if (Input.GetButtonUp("Sprint"))
        {
            isRunning = false;
        }
        // Crouch Key Pressed?
        if (Input.GetButtonDown("Crouch"))
        {
            isCrouching = true;
        }
        // Crouch Key Released?
        if (Input.GetButtonUp("Crouch"))
        {
            isCrouching = false;
        }

        // Melee Key Pressed and not Running and not Walking?
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isRunning && !isWalking)
        {
            isMeleePressed = true;
        }
        // Shoot Key Pressed?
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            isShootPressed = true;
        }
        // Throw Key Pressed and not Running and not Walking?
        if (Input.GetKeyDown(KeyCode.F) && !isRunning && !isWalking)
        {
            isThrowPressed = true;
        }
    }

    private void FixedUpdate()
    {
        if (pCtrl.GetisGrounded() && !pAnimHandler.isMelee && !pAnimHandler.isShooting && !pAnimHandler.isThrowing)
        {
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
            else
            {
                isWalking = false;
                isIdle = true;
            }
        }
    }

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
