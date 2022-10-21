using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Component Variables
    private PlayerController pCtrl;

    [Header("Movement Variables")]
    [SerializeField] private float verticalClimbSpeed;
    [SerializeField] internal float horizontalClimbSpeed;
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float horizontalSprintSpeed;
    internal bool facingRight = true;
    float scale;

    [Header("----- Jump Variables -----")]
    [SerializeField] float jumpVelocity;
    [SerializeField] float jumpTime;
    [SerializeField] float gravityScale;
    [SerializeField] float coyoteTime = 0.2f;
    [SerializeField] float jumpBufferTime = 0.2f;
    float jumpTimeCounter;
    float coyoteTimeCounter;
    float jumpBufferCounter;
    internal bool isMoving = true;
    internal bool isJumping;
    internal bool isClimbing;

    // Start is called before the first frame update
    private void Start()
    {
        pCtrl = GetComponent<PlayerController>();
        jumpTimeCounter = jumpTime;
        scale = transform.localScale.x;
    }
    private void Update()
    {
        // If game over stop player movement
        if (!pCtrl.gameOver)
        {
            if(!pCtrl.pInput.isCrouching && !pCtrl.pAnimHandler.takingDamage)
            {
                if (pCtrl.pInput.isWalking)
                    HorizontalMovement(horizontalSpeed);
                if (pCtrl.pInput.isRunning)
                    HorizontalMovement(horizontalSprintSpeed);
                if (pCtrl.pInput.isClimbing)
                {
                    Climb();  
                }
            }
            if(!pCtrl.pInput.isClimbing && pCtrl.rb.gravityScale == 0)
            {
                pCtrl.rb.gravityScale = gravityScale;
            }
            Jump();
        }
    }

    void Jump()
    {
        if (pCtrl.pColl.isGrounded())
            coyoteTimeCounter = coyoteTime;
        else
            coyoteTimeCounter -= Time.deltaTime;
        if (Input.GetButtonDown("Jump"))
            jumpBufferCounter = jumpBufferTime;
        else
            jumpBufferCounter -= Time.deltaTime;
        // Jump Key Pressed?
        if (jumpBufferCounter > 0f && coyoteTimeCounter > 0f || pCtrl.pInput.isClimbing)
        {
            pCtrl.rb.velocity = new Vector2(pCtrl.rb.velocity.x, jumpVelocity);
            jumpTimeCounter = jumpTime;
            gameManager.instance.soundManager.aud.PlayOneShot(gameManager.instance.soundManager.jump);
            jumpBufferCounter = 0f;
            isJumping = true;
            isClimbing = false;
        }
        // Checking for release of jump key and resetting jump timer
        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
            coyoteTimeCounter = 0f;
        }
        if(Input.GetButton("Jump") && isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                pCtrl.rb.velocity = new Vector2(pCtrl.rb.velocity.x, jumpVelocity);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
    }
    void Climb()
    {
        HorizontalMovement(horizontalClimbSpeed);
        if (pCtrl.yAxis > 0)
        {
            pCtrl.rb.velocity = new Vector2(pCtrl.rb.velocity.x, verticalClimbSpeed);
        }
        else if (pCtrl.yAxis < 0)
        {
            pCtrl.rb.velocity = new Vector2(pCtrl.rb.velocity.x, -verticalClimbSpeed);
        }
        else if (pCtrl.pInput.isClimbing && pCtrl.yAxis == 0 && pCtrl.xAxis == 0)
        {
            pCtrl.rb.gravityScale = 0;
            pCtrl.rb.velocity = Vector2.zero;
        }
    }

    void HorizontalMovement(float speed)
    {
        if (pCtrl.xAxis < 0)
        {
            facingRight = false;
            pCtrl.rb.velocity = new Vector2(-speed, pCtrl.rb.velocity.y);
            transform.localScale = new Vector2(-scale, scale);
        }
        else if (pCtrl.xAxis > 0)
        {
            facingRight = true;
            pCtrl.rb.velocity = new Vector2(speed, pCtrl.rb.velocity.y);
            transform.localScale = new Vector2(scale, scale);
        }
        else
        {
            pCtrl.rb.velocity = new Vector2(0, pCtrl.rb.velocity.y);
        }
    }
};
