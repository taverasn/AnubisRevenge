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
    internal bool takingDamage;
    
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
    private const string PLAYER_CLIMB = "Player_Climb";
    // Start is called before the first frame update
    void Start()
    {
        pCtrl = GetComponent<PlayerController>();
    }

    public void ChangeAnimationState(string newState, float _normalizedTime = 0)
    {
        // stop same animation from interrupting itself
        if (currentState == newState) return;

        // play the animation
        if (_normalizedTime == 0)
        {
            pCtrl.anim.Play(newState);
        }
        else
        {
            pCtrl.anim.Play(newState, 0, _normalizedTime);
        }
        // reassign the current state
        currentState = newState;
    }

    // Update is called once per frame

    private void Update()
    {
        if (!pCtrl.gameOver)
        {
            pCtrl.anim.speed = 1;
            if (takingDamage && pCtrl.pInput.isClimbing && currentState == PLAYER_HURT)
            {
                pCtrl.pInput.isClimbing = false;
                takingDamage = false;
            }
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
        if (pCtrl.isDamaged)
        {
            pCtrl.isDamaged = false;
            if (!takingDamage)
            {
                takingDamage = true;
                ChangeAnimationState(PLAYER_HURT);
            }
            attackDelay = pCtrl.anim.GetCurrentAnimatorStateInfo(0).length;
            // calls Function after time of attack delay
            Invoke("TakingDamageComplete", attackDelay);
        }
    }
    void AttackAnimations()
    {
        // Manage animation change when switching between ground and air attacks
        if (pCtrl.pColl.isGrounded())
        {
            if (currentState == PLAYER_JUMPMELEE)
            {
                ChangeAnimationState(PLAYER_MELEE, pCtrl.anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
            }
            else if (currentState == PLAYER_JUMPSHOOT)
            {
                ChangeAnimationState(PLAYER_SHOOT, pCtrl.anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
            }
            else if (currentState == PLAYER_JUMPTHROW)
            {
                ChangeAnimationState(PLAYER_THROW, pCtrl.anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
            }
            if(pCtrl.pMove.isMoving)
            {
                if(currentState == PLAYER_SHOOT)
                {
                    if (pCtrl.pInput.isWalking)
                        ChangeAnimationState(PLAYER_WALKSHOOT, pCtrl.anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
                    else if (pCtrl.pInput.isRunning)
                        ChangeAnimationState(PLAYER_RUNSHOOT, pCtrl.anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
                }
                if(currentState == PLAYER_WALKSHOOT)
                {
                    if(pCtrl.pInput.isIdle)
                        ChangeAnimationState(PLAYER_SHOOT, pCtrl.anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
                    else if(pCtrl.pInput.isRunning)
                        ChangeAnimationState(PLAYER_RUNSHOOT, pCtrl.anim.GetCurrentAnimatorStateInfo(0).normalizedTime);

                }
                if(currentState == PLAYER_RUNSHOOT)
                {
                    if(pCtrl.pInput.isIdle)
                        ChangeAnimationState(PLAYER_SHOOT, pCtrl.anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
                    else if(pCtrl.pInput.isWalking)
                        ChangeAnimationState(PLAYER_WALKSHOOT, pCtrl.anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
                }
            }
        }        
        else if (!pCtrl.pColl.isGrounded())
        {
            if (currentState == PLAYER_MELEE)
            {
                ChangeAnimationState(PLAYER_JUMPMELEE, pCtrl.anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
            }
            else if (currentState == PLAYER_SHOOT)
            {
                ChangeAnimationState(PLAYER_JUMPSHOOT, pCtrl.anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
            }
            else if (currentState == PLAYER_THROW)
            {
                ChangeAnimationState(PLAYER_JUMPTHROW, pCtrl.anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
            }
        }
        // Melee Pressed?
        if (pCtrl.pInput.isMeleePressed && pCtrl.pAttack.isMelee)
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
                else if (!pCtrl.pColl.isGrounded())
                {
                    ChangeAnimationState(PLAYER_JUMPMELEE);
                }
                else if (pCtrl.pInput.isIdle)
                {
                    ChangeAnimationState(PLAYER_MELEE);
                    pCtrl.pMove.isMoving = false;
                }
            }
            // attack delay set to the length in seconds of the current animation
            attackDelay = pCtrl.anim.GetCurrentAnimatorStateInfo(0).length;
            // calls Function after time of attack delay
            Invoke("MeleeAttackComplete", attackDelay);
        }
        else if (pCtrl.pInput.isThrowPressed && pCtrl.pAttack.GetThrown())
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
                else if (!pCtrl.pColl.isGrounded())
                {
                    ChangeAnimationState(PLAYER_JUMPTHROW);
                }
                else if (pCtrl.pInput.isIdle)
                {
                    ChangeAnimationState(PLAYER_THROW);
                    pCtrl.pMove.isMoving = false;
                }
            }
            // attack delay set to the length in seconds of the current animation
            attackDelay = pCtrl.anim.GetCurrentAnimatorStateInfo(0).length;
            // calls Function after time of attack delay
            Invoke("ThrowAttackComplete", attackDelay);
        }
        else if (pCtrl.pInput.isShootPressed && pCtrl.pAttack.isShooting)
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
                else if (!pCtrl.pColl.isGrounded())
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
        Debug.Log("damage complete");
        // When health is <= to 0 the player death animation will take place and gameover is set to true causing all player input to stop
        if (pCtrl.HP <= 0)
        {
            ChangeAnimationState(PLAYER_DEATH);
            pCtrl.gameOver = true;
        }
    }
    void ThrowAttackComplete()
    {
        isThrowing = false;
        pCtrl.pMove.isMoving = true;
    }
    void ShootAttackComplete()
    {
        isShooting = false;
        pCtrl.pMove.isMoving = true;
    }
    void MeleeAttackComplete()
    {
        isMelee = false;
        pCtrl.pMove.isMoving = true;
    }

    // Function to control animations for Walking, Running and Jumping
    void MovementAnimations()
    {
        // Player is not attacking?
        if (!isMelee && !isShooting && !isThrowing)
        {
            // Player Collider hitting Ground Collider
            if (!pCtrl.pInput.isClimbing)
            {
                if (pCtrl.pColl.isGrounded())
                {
                    // Crouch not pressed?
                    if (!pCtrl.pInput.isCrouching)
                    {
                        // Player X Input != 0?
                        if (!pCtrl.pInput.isIdle && pCtrl.xAxis != 0)
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
            else if (pCtrl.pInput.isClimbing)
            {
                ChangeAnimationState(PLAYER_CLIMB);
                if (pCtrl.yAxis == 0 && currentState == PLAYER_CLIMB)
                    pCtrl.anim.speed = 0;
            }
        }
    }
}

