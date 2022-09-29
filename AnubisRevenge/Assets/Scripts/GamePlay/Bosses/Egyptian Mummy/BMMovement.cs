using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BMMovement : MonoBehaviour
{ 
    BMAttacking cool;
    public Animator animator;
    private string currentState;
    public float speed;
    private Transform playerPos;

    const string BM_IDLE = "BM_Idle";
    const string BM_WALK = "BM_Walk";
    void Start()
    {
        speed = 3.0f;
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        cool = GetComponent<BMAttacking>();
    }

    void Update()
    {
        if (speed == 3.0f)
        {
            Debug.Log("Part 1");
            ChangeAnimationState(BM_WALK);
            Vector2 target = new Vector2(playerPos.position.x, animator.transform.position.y);
            animator.transform.position = Vector2.MoveTowards(animator.transform.position, target, speed * Time.deltaTime);
        }
        else if (cool.onCoolDown == true)
        {
            Debug.Log("Part 2");
            ChangeAnimationState(BM_IDLE);
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
}
