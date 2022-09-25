using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    private Animator animator;
    private PlayerController pCtrl;
    private PlayerInput pInput;
    private string currentState;
    public bool isMelee;
    public bool isShooting;
    public bool isThrowing;
    private float attackDelay;


    //Animation States
    private const string PLAYER_IDLE = "Player_Idle";
    private const string PLAYER_RUN = "Player_Run";
    private const string PLAYER_WALK = "Player_Walk";
    private const string PLAYER_JUMP = "Player_Jump";
    private const string PLAYER_CROUCH = "Player_Crouch";
    private const string PLAYER_CROUCHMELEE = "Player_CrouchMelee";
    private const string PLAYER_MELEE = "Player_Melee";
    private const string PLAYER_JUMPMELEE = "Player_JumpMelee";
    private const string PLAYER_CROUCHSHOOT = "Player_CrouchShoot";
    private const string PLAYER_CROUCHTHROW = "Player_CrouchThrow";
    private const string PLAYER_SHOOT = "Player_Shoot";
    private const string PLAYER_THROW = "Player_Throw";
    private const string PLAYER_JUMPSHOOT = "Player_JumpShoot";
    private const string PLAYER_JUMPTHROW = "Player_JumpThrow";
    private const string PLAYER_WALKSHOOT = "Player_WalkShoot";
    private const string PLAYER_RUNSHOOT = "Player_RunShoot";
    // Start is called before the first frame update
    void Start()
    {
        pInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
        pCtrl = GetComponent<PlayerController>();
    }

    public void ChangeAnimationState(string newState)
    {
        // stop same animation from interrupting itself
        if (currentState == newState) return;

        // play the animation
        animator.Play(newState);

        // reassign the current state
        currentState = newState;
    }

    // Update is called once per frame
    void Update()
    {
        MovementAnimations();
    }

    private void FixedUpdate()
    {
        AttackAnimations();
    }
    void AttackAnimations()
    {
        if (pInput.GetisMeleePressed())
        {
            pInput.SetisMeleePressed(false);
            if (!isMelee)
            {
                isMelee = true;
                if (pInput.GetisCrouching())
                {
                    ChangeAnimationState(PLAYER_CROUCHMELEE);
                }
                else if (!pCtrl.GetisGrounded())
                {
                    ChangeAnimationState(PLAYER_JUMPMELEE);
                }
                else if (pInput.GetisIdle())
                {
                    ChangeAnimationState(PLAYER_MELEE);
                }
            }
            attackDelay = animator.GetCurrentAnimatorStateInfo(0).length;
            Invoke("MeleeAttackComplete", attackDelay);
        }
        else if (pInput.GetisThrowPressed())
        {
            pInput.SetisThrowPressed(false);
            if(!isThrowing)
            {
                isThrowing = true;
                if (pInput.GetisCrouching())
                {
                    ChangeAnimationState(PLAYER_CROUCHTHROW);
                }
                else if (!pCtrl.GetisGrounded())
                {
                    ChangeAnimationState(PLAYER_JUMPTHROW);
                }
                else if (pInput.GetisIdle())
                {
                    ChangeAnimationState(PLAYER_THROW);
                }
            }
            attackDelay = animator.GetCurrentAnimatorStateInfo(0).length;
            Invoke("ThrowAttackComplete", attackDelay);
        }
        else if (pInput.GetisShootPressed())
        {
            pInput.SetisShootPressed(false);
            if(!isShooting)
            {
                isShooting = true;
                if(pInput.GetisCrouching())
                {
                    ChangeAnimationState(PLAYER_CROUCHSHOOT);
                }
                else if (!pCtrl.GetisGrounded())
                {
                    ChangeAnimationState(PLAYER_JUMPSHOOT);
                }
                else if (pInput.GetisIdle())
                {
                    ChangeAnimationState(PLAYER_SHOOT);
                }
                else if (pInput.GetisWalking())
                {
                    ChangeAnimationState(PLAYER_WALKSHOOT);
                }
                else if (pInput.GetisRunning())
                {
                    ChangeAnimationState(PLAYER_RUNSHOOT);
                }
            }
            attackDelay = animator.GetCurrentAnimatorStateInfo(0).length;
            Invoke("ShootAttackComplete", attackDelay);
        }
    }
    void ThrowAttackComplete()
    {
        isThrowing = false;
    }
    void ShootAttackComplete()
    {
        isShooting = false;
    }
    void MeleeAttackComplete()
    {
        isMelee = false;
    }
    void MovementAnimations()
    {
        if (!isMelee && !isShooting && !isThrowing)
        {
            if (pCtrl.GetisGrounded())
            {
                if (!pInput.GetisCrouching())
                {
                    if (!pInput.GetisIdle())
                    {
                        if (pInput.GetisWalking())
                        {
                            ChangeAnimationState(PLAYER_WALK);
                        }
                        else
                        {
                            ChangeAnimationState(PLAYER_RUN);
                        }
                    }
                    else
                    {
                        ChangeAnimationState(PLAYER_IDLE);
                    }
                }
                else
                {
                    ChangeAnimationState(PLAYER_CROUCH);
                }
            }
            else
            {
                ChangeAnimationState(PLAYER_JUMP);
            }
        }
    }
}

