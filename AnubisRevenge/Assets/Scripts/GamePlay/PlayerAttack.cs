using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Animator animator;
    public Transform projectileSpawnPoint;

    private float timeBtwMAttack;
    public float startTimeMBtwAttack;
    private float timeBtwRAttack;
    public float startTimeRBtwAttack;

    private PlayerController player;
    public Transform attackPos;
    public float meleeAttackRange;
    public LayerMask whatIsEnemies;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerCharacter").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Melee
        
        if (Input.GetKeyDown(KeyCode.Mouse0) && timeBtwMAttack > startTimeMBtwAttack)
        {
            timeBtwMAttack = 0;
            animator.SetTrigger("Melee");
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, meleeAttackRange, whatIsEnemies);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<Health>().TakeDamage(damage);
            }

        }
        timeBtwMAttack += Time.deltaTime;

        // Throw
        if (Input.GetKeyDown(KeyCode.F))
        {
            animator.SetTrigger("Throw");
        }

        // Shoot
        if (Input.GetKeyDown(KeyCode.Mouse1) && timeBtwRAttack > startTimeRBtwAttack)
        {
            timeBtwRAttack = 0;
            animator.SetTrigger("isShooting");
            Instantiate(projectilePrefab, projectileSpawnPoint.position, player.transform.rotation);


        }
        timeBtwRAttack += Time.deltaTime;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, meleeAttackRange);
    }
}
