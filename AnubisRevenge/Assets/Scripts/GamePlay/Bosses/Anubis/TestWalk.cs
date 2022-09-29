using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWalk : MonoBehaviour
{
    private Animator animator;
    public bool isFlipped = false;
    public bool isWalking;
    public bool isRunning;
    Transform player;
    private AnubisAttacks AA;
    Rigidbody2D rb;
    private string currentState;
    const string ANUBIS_IDLE = "Anubis_Idle";
    const string ANUBIS_WALK = "Anubis_Walk";
    const string ANUBIS_RUN = "Anubis_Run";
    private Transform playerPos;
    [SerializeField] float speed;

    void Start()
    {
        AA = GetComponent<AnubisAttacks>();
       animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
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
   
    void Update()
    {


        LookAtPlayer();
        if (speed > 3 && !AA.canAttack)
        {
            isRunning = true;
        }
        /*else
        {
            isWalking = true;
        }*/
    }
    void FixedUpdate()
    {
        //For potiental Second Phase
        if (isRunning)
        {
            //Follows the player
            Vector2 target = new Vector2(player.position.x, transform.position.y);
            Vector2 newPos = Vector2.MoveTowards(transform.position, target, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
            ChangeAnimationState(ANUBIS_RUN);
        }
        /*else
        {
            Vector2 target = new Vector2(player.position.x, transform.position.y);
            Vector2 newPos = Vector2.MoveTowards(transform.position, target, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
            ChangeAnimationState(ANUBIS_WALK);
        }*/
        
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }
}
