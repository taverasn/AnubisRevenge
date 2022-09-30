using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    private PlayerController pCtrl;
    
    private string currentState;

    internal bool isMelee;
    internal bool isShooting;
    internal bool isThrowing;
    private bool takingDamage;
    
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
    private const string PLAYER_DEATH = "Player_Death";
    private const string PLAYER_HURT = "Player_Hurt";
    // Start is called before the first frame update
    void Start()
    {
        pCtrl = GetComponent<PlayerController>();
    }

    public void ChangeAnimationState(string newState)
    {
        // stop same animation from interrupting itself
        if (currentState == newState) return;

        // play the animation
        pCtrl.anim.Play(newState);

        // reassign the current state
        currentState = newState;
    }

    // Update is called once per frame

    private void Update()
    {
        if (!pCtrl.gameOver)
        {
            DamagedAnimations();
            if (!takingDamage)
            {
                MovementAnimations();
                AttackAnimations();
            }
        }
    }
    void DamagedAnimations()
    {
        if (pCtrl.pHealth.isDamaged)
        {
            pCtrl.pHealth.isDamaged = false;
            if (!takingDamage)
            {
                takingDamage = true;
                if (pCtrl.pHealth.currentHealth > 0)
                {
                    ChangeAnimationState(PLAYER_HURT);
                }
            }
            attackDelay = pCtrl.anim.GetCurrentAnimatorStateInfo(0).length;
            // calls Function after time of attack delay
            Invoke("TakingDamageComplete", attackDelay);
        }
    }
    void AttackAnimations()
    {
        // Melee Pressed?
        if (pCtrl.pInput.isMeleePressed)
        {
            pCtrl.pInput.isMeleePressed = false;
            if (!isMelee)
            {
                isMelee = true;
                // changes melee animations based on input
                if (pCtrl.pInput.isCrouching)
                {
                    ChangeAnimationState(PLAYER_CROUCHMELEE);
                }
                else if (!pCtrl.pColl.isGrounded)
                {
                    ChangeAnimationState(PLAYER_JUMPMELEE);
                }
                else if (pCtrl.pInput.isIdle)
                {
                    ChangeAnimationState(PLAYER_MELEE);
                }
            }
            // attack delay set to the length in seconds of the current animation
            attackDelay = pCtrl.anim.GetCurrentAnimatorStateInfo(0).length;
            // calls Function after time of attack delay
            Invoke("MeleeAttackComplete", attackDelay);
        }
        else if (pCtrl.pInput.isThrowPressed)
        {
            pCtrl.pInput.isThrowPressed = false;
            // changes Throw animations based on input
            if(!isThrowing)
            {
                isThrowing = true;
                if (pCtrl.pInput.isCrouching)
                {
                    ChangeAnimationState(PLAYER_CROUCHTHROW);
                }
                else if (!pCtrl.pColl.isGrounded)
                {
                    ChangeAnimationState(PLAYER_JUMPTHROW);
                }
                else if (pCtrl.pInput.isIdle)
                {
                    ChangeAnimationState(PLAYER_THROW);
                }
            }
            // attack delay set to the length in seconds of the current animation
            attackDelay = pCtrl.anim.GetCurrentAnimatorStateInfo(0).length;
            // calls Function after time of attack delay
            Invoke("ThrowAttackComplete", attackDelay);
        }
        else if (pCtrl.pInput.isShootPressed)
        {
            pCtrl.pInput.isShootPressed = false;
            if(!isShooting)
            {
                isShooting = true;
                // Changes Shoot animation based on user input
                if(pCtrl.pInput.isCrouching)
                {
                    ChangeAnimationState(PLAYER_CROUCHSHOOT);
                }
                else if (!pCtrl.pColl.isGrounded)
                {
                    ChangeAnimationState(PLAYER_JUMPSHOOT);
                }
                else if (pCtrl.pInput.isIdle)
                {
                    ChangeAnimationState(PLAYER_SHOOT);
                }
                else if (pCtrl.pInput.isWalking)
                {
                    ChangeAnimationState(PLAYER_WALKSHOOT);
                }
                else if (pCtrl.pInput.isRunning)
                {
                    ChangeAnimationState(PLAYER_RUNSHOOT);
                }
            }
            
            // attack delay set to the length in seconds of the current animation
            attackDelay = pCtrl.anim.GetCurrentAnimatorStateInfo(0).length;
            // calls Function after time of attack delay
            Invoke("ShootAttackComplete", attackDelay);
        }
    }

    // Functions to break out of animation loops
    void TakingDamageComplete()
    {
        takingDamage = false;
        // When health is <= to 0 the player death animation will take place and gameover is set to true causing all player input to stop
        if (pCtrl.pHealth.currentHealth <= 0)
        {
            ChangeAnimationState(PLAYER_DEATH);
            pCtrl.gameOver = true;
        }
    }
    void ThrowAttackComplete()
    {
        isThrowing = false;
        ChangeAnimationState(PLAYER_IDLE);

    }
    void ShootAttackComplete()
    {
        isShooting = false;
        ChangeAnimationState(PLAYER_IDLE);

    }
    void MeleeAttackComplete()
    {
        isMelee = false;
        ChangeAnimationState(PLAYER_IDLE);

    }

    // Function to control animations for Walking, Running and Jumping
    void MovementAnimations()
    {
        // Causes the player to change to Idle state when coming in contact with the ground during an animation
        if (currentState == PLAYER_JUMPMELEE || currentState == PLAYER_JUMPSHOOT || currentState == PLAYER_JUMPTHROW)
            if (pCtrl.pColl.isGrounded)
                ChangeAnimationState(PLAYER_IDLE);
        // Player is not attacking?
        if (!isMelee && !isShooting && !isThrowing)
        {
            // Player Collider hitting Ground Collider
            if (pCtrl.pColl.isGrounded)
            {
                // Crouch not pressed?
                if (!pCtrl.pInput.isCrouching)
                {
                    // Player X Input != 0?
                    if (!pCtrl.pInput.isIdle)
                    {
                        // Walk pressed?
                        if (pCtrl.pInput.isWalking)
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

