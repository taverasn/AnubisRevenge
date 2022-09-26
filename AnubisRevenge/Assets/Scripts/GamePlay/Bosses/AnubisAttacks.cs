using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnubisAttacks : MonoBehaviour
{
    Health playerHealth;
    public GameObject player;
    private string currentState;
    private Animator animator;
    private float cooldownTimer = Mathf.Infinity;
    private TestWalk testwalk;
    public Transform attackPos;
    public LayerMask whatisPlayer;
    public float ARange;

    //Bools
    public bool inRange;
    public bool attackRange;
    public bool canAttack;
    //Animations
    const string ANUBIS_IDLE = "Anubis_Idle";
    const string ANUBIS_SLASH = "Anubis_Slash";
    const string ANUBIS_RUNSLASH = "Anubis_RunSlash";
    const string ANUBIS_IDLE2 = "Anubis_Phase2Idle";

    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private PolygonCollider2D polygonCollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerHealth = GameObject.Find("PlayerCharacter").GetComponent<Health>();
    }
    void Update()
    {
        cooldownTimer += Time.deltaTime;
        //PlayerInSight();
        distanceCheck();
        if (inRange)
        {
            inRange = false;
            ChangeAnimationState(ANUBIS_IDLE);
            if (!canAttack)
            {
                ChangeAnimationState(ANUBIS_SLASH);
                Collider2D[] PlayerToDamage = Physics2D.OverlapCircleAll(attackPos.position, ARange, whatisPlayer);
                for (int i = 0; i < PlayerToDamage.Length; i++)
                {
                    PlayerToDamage[i].GetComponent<Health>().TakeDamage(damage);
                }
                canAttack = true;
            }
            Invoke("AttackComplete", 0.35f);
        }
    }
   

    private void distanceCheck()
    {
        if (player.transform.position.x - transform.position.x < 10)
        {
            inRange = true;
        }
    }


    private void PlayerInSight()
    {

        Collider2D[] PlayerToDamage = Physics2D.OverlapCircleAll(attackPos.position, ARange, whatisPlayer);
        for (int i = 0; i < PlayerToDamage.Length; i++)
        {
            PlayerToDamage[i].GetComponent<Health>().TakeDamage(damage);
        }
       
        /*RaycastHit2D hit = Physics2D.BoxCast(polygonCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector2(polygonCollider.bounds.size.x * range, polygonCollider.bounds.size.y),
            0, Vector2.left, 0, playerLayer);

        if (hit.collider != null && hit.transform.tag == "Player")
        {
            playerHealth = hit.transform.GetComponent<Health>();
            inRange = true;
            DamagePlayer();
        }
        else if (hit.collider != null && hit.transform.tag != "Player")
        {
            inRange = false;
            ChangeAnimationState(ANUBIS_IDLE);
        }*/

    }

   /* private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(polygonCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(polygonCollider.bounds.size.x * range, polygonCollider.bounds.size.y, polygonCollider.bounds.size.z));
    }*/

    void ChangeAnimationState(string newState)
    {
        // stop same animation from interrupting itself
        if (currentState == newState) return;

        // play the animation
        animator.Play(newState);

        // reassign the current state
        currentState = newState;
    }
    
    /*private void DamagePlayer()
    {
        if (inRange)
            playerHealth.TakeDamage(damage);
        //ChangeAnimationState(ANUBIS_IDLE2);
    }
*/
    private void AttackComplete()
    {
        canAttack = false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, ARange);
    }
}