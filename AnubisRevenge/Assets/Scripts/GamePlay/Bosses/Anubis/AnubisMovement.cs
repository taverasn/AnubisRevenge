using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnubisMovement : StateMachineBehaviour
{
    public float maxTime;
    public float minTime;

    public float speed;
    private Transform playerPos;
    public float timer;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        timer = Random.Range(minTime, maxTime);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (speed <= 3.0f)
        {
            animator.SetBool("Walk", true);
        }
        else if (speed > 3)
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Run", true);
        }

        Vector2 target = new Vector2(playerPos.position.x, animator.transform.position.y);
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, target, speed * Time.deltaTime);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
    
    public void FollowPlayer()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
}

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anubis : StateMachineBehaviour
{
    public float timer;
    public float maxTime;
    public float minTime;
   
    public float speed;
    private Transform playerPos;

   override public void OnStateEnter(Animator animator, AnimatorStateInfo, stateInfo, int layerIndex) {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        timer = Random.Range(minTime, maxTime);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo, stateInfo, int layerIndex) {
        if(timer <= 0)
        {
            animator.Settrigger("Anubis_Walk");
        }
        else
        {
            timer -= Time.deltaTime;
        }

        Vector2 target = new Vector2(playerPos.position.x, animator.transform.position.y);
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, target, speed * Time.deltaTime);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo, stateInfo, int layerIndex)
    {

    }
}
*/

/*[SerializedField]
    private float speed = 5f;

    private animator animator;

    //Animations 
    const string ANUBIS_IDLE = "Anubis_Idle";
    const string ANUBIS_WALK = "Anubis_Walk";
    const string ANUBIS_RUN = "Anubis_Run";
    // Attack Animations
    const string ANUBIS_SLASH = "Anubis_Slash";
    const string ANUBIS_RunSLASH = "Anubis_RunSlash";
    const string ANUBIS_KICK = "Anubis_Kick";
    //Directions
    private float xAxis;
    private float yAxis;
    //Decisions
    private int Rand;


    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isJumpTriggered;
    private string currentAnimation;
    private int groundMask;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        groundMask = 1 << LayerMask.NameToLayer("Default");
    }
    void Update()
    {
        Rand = Random.Range(0, 2);
    }*/
