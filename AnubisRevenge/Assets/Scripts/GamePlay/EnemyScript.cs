using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float visionRange;
    [SerializeField] float speed;
    [SerializeField] float scaleX;
    [SerializeField] float scaleY;
    private Vector2 velocity;
    [SerializeField] private Animator anim;

    private Rigidbody2D body;
    
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;
    [SerializeField] private Transform attackPos;

    [Header("Collider Parameters")]
    [SerializeField] private PolygonCollider2D polygonCollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;


    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        cooldownTimer += Time.deltaTime;
        float enemyDist = Vector2.Distance(transform.position, player.position);

        if (transform.position.y > player.position.y)
        {
            anim.SetBool("moving", false);
            RemainIdle();
        }
        else
        {
            if (enemyDist < visionRange)
            {
                LookAtPlayer();
                FollowPlayer();
                anim.SetBool("moving", true);
                if (cooldownTimer >= attackCooldown)
                {
                    cooldownTimer = 0;
                    Collider2D[] playerToDamage = Physics2D.OverlapCircleAll(attackPos.position, range, playerLayer);
                    anim.SetTrigger("meleeAttack");
                    for (int i = 0; i < playerToDamage.Length; i++)
                    {
                        playerToDamage[i].GetComponent<PlayerController>().takeDamage(damage);
                    }
                }
            }
            else
            {
                anim.SetBool("moving", false);
                RemainIdle();
            }
        }
    }
    
    void FollowPlayer()
    {
        //If the enemy is to the left or right of the player
        if(transform.position.x < player.position.x)
        {
            velocity.x = speed;
        }
        else
        {
            velocity.x = -speed;
        }
        body.velocity = velocity;
    }
    
    void RemainIdle()
    {
        body.velocity = new Vector2(0,0);
    }

    public void LookAtPlayer()
    {
        if (transform.position.x < player.position.x) 
        {
            transform.localScale = new Vector2(scaleX, scaleY);
        }
        else if (transform.position.x > player.position.x) 
        {
            transform.localScale = new Vector2(-scaleX, scaleY);
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, range);
    }
}
