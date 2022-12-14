using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnubisMovement : MonoBehaviour
{
    BossHealth BossHP;
    AnubisAttacks cool;
    public Animator animator;
    private string currentState;
    public float speed;
    private bool isFlipped;
    private Transform playerPos;

    const string ANUBIS_IDLE = "Anubis_Idle";
    const string ANUBIS_WALK = "Anubis_Walk";

    void Start()
    {
        BossHP = GetComponent<BossHealth>();
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        cool = GetComponent<AnubisAttacks>();
    }

    void Update()
    {
        if (!BossHP.dead)
        {
            LookAtPlayer();
            if (speed == 5.0f)
            {
                FollowPlayer();
            }
            else if (cool.onCoolDown == true)
            {
                ChangeAnimationState(ANUBIS_IDLE);
            }
            else
            {
                speed = 5.0f;
                FollowPlayer();
            }
        }
    }
    void ChangeAnimationState(string newState)
    {
        // stop same animation from interrupting itself
        if (currentState == newState) return;

        // play the animation
        animator.Play(newState);

        // reassign the current state
        currentState = newState;
    }

    void FollowPlayer()
    {
        if (!BossHP.dead)
        {
            ChangeAnimationState(ANUBIS_WALK);
            Vector2 target = new Vector2(playerPos.position.x, animator.transform.position.y);
            animator.transform.position = Vector2.MoveTowards(animator.transform.position, target, speed * Time.deltaTime);
        }
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > playerPos.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < playerPos.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }
}