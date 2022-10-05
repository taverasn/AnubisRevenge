using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float visionRange;
    [SerializeField] float speed;

    [SerializeField] private Animator anim;

    private Rigidbody2D body;
    private bool isFlipped;
    
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private PolygonCollider2D polygonCollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    private Health playerHealth;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        playerHealth = GameObject.Find("PlayerCharacter").GetComponent<Health>();
    }


    void Update()
    {
        cooldownTimer += Time.deltaTime;
        float enemyDist = Vector2.Distance(transform.position, player.position);

        if(enemyDist < visionRange)
        {
            LookAtPlayer();
            FollowPlayer();
            anim.SetBool("moving", true);
            if (PlayerInSight())
            {
                if (cooldownTimer >= attackCooldown)
                {
                    cooldownTimer = 0;
                    anim.SetTrigger("meleeAttack");
                }
            }
        }
        else
        {
            anim.SetBool("moving", false);
            RemainIdle();
        }
    }
    void FollowPlayer()
    {
        //If the enemy is to the left or right of the player
        if(transform.position.x < player.position.x)
        {
            body.velocity = new Vector2(speed, 0);
        }
        else
        {
            body.velocity = new Vector2(-speed, 0);
        }
    }
    
    void RemainIdle()
    {
        body.velocity = new Vector2(0,0);
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x < player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x > player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(polygonCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(polygonCollider.bounds.size.x * range, polygonCollider.bounds.size.y, polygonCollider.bounds.size.z));
    }

    private bool PlayerInSight()
    {

        RaycastHit2D hit =
            Physics2D.BoxCast(polygonCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector2(polygonCollider.bounds.size.x * range, polygonCollider.bounds.size.y),
            0, Vector2.left, 0, playerLayer);

        if (hit.collider != null && hit.transform.tag == "Player")
            playerHealth = hit.transform.GetComponent<Health>();

        return hit.collider != null;
    }

    private void DamagePlayer()
    {
        if (PlayerInSight())
            playerHealth.TakeDamage(damage);
    }
}
