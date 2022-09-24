using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    private Animator animator;
    private PlayerController pCtrl;
    private PlayerInput pInput;
    private PlayerAttack pAttack;
    private string currentState;

    //Animation States
    private const string PLAYER_IDLE = "Player_Idle";
    private const string PLAYER_RUN = "Player_Run";
    private const string PLAYER_WALK = "Player_Walk";
    private const string PLAYER_JUMP = "Player_Jump";
    private const string PLAYER_CROUCH = "Player_Crouch";

    // Start is called before the first frame update
    void Start()
    {
        pInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
        pCtrl = GetComponent<PlayerController>();
        pAttack = GetComponent<PlayerAttack>();
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
        if (!pAttack.isMelee && !pAttack.isShooting && !pAttack.isThrowing)
        {
            if(pCtrl.GetisGrounded())
            {
                if(!pInput.GetisCrouching())
                {
                    if(!pInput.GetisIdle())
                    {
                        if(pInput.GetisWalking())
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
