using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BMAttacking : MonoBehaviour
{
    //public GameObject player;
    //public Transform attackPos
    //public bool inRange;
    //public LayerMask whatisPlayer;
    //public float ARange;

    Health playerHealth;
    public Animator animator;
    private string currentState;
    const string player = "PlayerCharacter";
    const string BM_IDLE = "BM_Idle";
    const string BM_ATTACK = "BM_Attack";
    
    public float delay;
    public Transform circleOrigin;
    public float radius;
    private bool onCoolDown;
    [SerializeField] private int damage;

    /* [Header("Attack Parameters")]
     [SerializeField] private float range;
     [SerializeField] private int damage;

     [Header("Collider Parameters")]
     [SerializeField] private float colliderDistance;
     [SerializeField] private PolygonCollider2D polygonCollider;

     [Header("Player Layer")]
     [SerializeField] private LayerMask playerLayer;
 */
    void Start()
    {
        animator = GetComponent<Animator>();
        playerHealth = GameObject.Find("PlayerCharacter").GetComponent<Health>();
    }

    void Update()
    {
        DetectPlayer();
        /*distanceCheck();
        if (inRange)
        {
            inRange = true;
            Debug.Log("Player In Range");
            ChangeAnimationState(BM_IDLE);
            if (onCoolDown)
            {
                Debug.Log("Player attacked");
                Collider2D[] PlayerToDamage = Physics2D.OverlapCircleAll(attackPos.position, ARange, whatisPlayer);
            }
            delayAttack();
        }*/
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

    public void attack()
    {
        if (onCoolDown)
        {
            return;
        }
        ChangeAnimationState(BM_ATTACK);
        Collider2D[] PlayerToDamage = Physics2D.OverlapCircleAll(circleOrigin.position, radius);

        /*for (int i = 0; i < PlayerToDamage.Length; i++)
        {
            PlayerToDamage[i].GetComponent<Health>().TakeDamage(damage);
        }*/
        onCoolDown = true;
        StartCoroutine(delayAttack());
    }

    private IEnumerator delayAttack()
    {
        ChangeAnimationState(BM_IDLE);
        yield return new WaitForSeconds(delay);
        onCoolDown = false;
    }

    /*private void distanceCheck()
    {
        if (player.transform.position.x - transform.position.x < 10)
        {
            inRange = true;
        }
    }*/

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Vector3 position = circleOrigin == null ? Vector3.zero : circleOrigin.position;
        Gizmos.DrawWireSphere(position, radius);
        //Gizmos.DrawWireSphere(attackPos.position, ARange);
    }
    public void DetectPlayer()
    {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(circleOrigin.position, radius))
        {
            if (collider.name == player)
            {
                attack();
            }
        }
    }
}
