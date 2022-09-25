using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerInput pInput;
    public GameObject projectilePrefab;
    public GameObject launchableProjectilePrefab;
    public Transform projectileSpawnPoint;
    public Transform launchableProjectileSpawnPoint;

    private float timeBtwMeleeAttack;
    public float startTimeMeleeBtwAttack;

    private float timeBtwRangeAttack;
    public float startTimeRangeBtwAttack; 
    
    private float timeBtwThrowAttack;
    public float startTimeThrowBtwAttack;

    private PlayerController pCtrl;
    public Transform attackPos;
    public float meleeAttackRange;
    public LayerMask whatIsEnemies;
    public int damage;
    private bool Thrown;
    private float throwDelay;
    public float throwDelayStartTime;


    private float attackDelay;
    // Animation States


    // Start is called before the first frame update
    void Start()
    {
        pInput = GetComponent<PlayerInput>();
        pCtrl = GameObject.Find("PlayerCharacter").GetComponent<PlayerController>();
    }

    private void Update()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Melee

        if (pInput.GetisMeleePressed() && timeBtwMeleeAttack > startTimeMeleeBtwAttack && !pInput.GetisRunning() && !pInput.GetisWalking())
        {
            timeBtwMeleeAttack = 0;
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, meleeAttackRange, whatIsEnemies);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<Health>().TakeDamage(damage);
            }
        }

        timeBtwMeleeAttack += Time.deltaTime;



        // Shoot
        if (pInput.GetisShootPressed() && timeBtwRangeAttack > startTimeRangeBtwAttack)
        {
            
            timeBtwRangeAttack = 0;
            Instantiate(projectilePrefab, projectileSpawnPoint.transform.position, transform.rotation);
        }
        timeBtwRangeAttack += Time.deltaTime;

        // Throw
        if (pInput.GetisThrowPressed() && timeBtwThrowAttack > startTimeThrowBtwAttack && !pInput.GetisRunning() && !pInput.GetisWalking())
        {
            timeBtwThrowAttack = 0;

            Thrown = true;
            throwDelay = 0;


        }
        timeBtwThrowAttack += Time.deltaTime;
        throwDelay += Time.deltaTime;
        if(Thrown == true && throwDelay > throwDelayStartTime)
        {
            if(pCtrl.facingRight == true)
            {
            Instantiate(launchableProjectilePrefab, launchableProjectileSpawnPoint.position, 
                Quaternion.Euler(0, 0, -38));
            } else
            {
                Instantiate(launchableProjectilePrefab, launchableProjectileSpawnPoint.position,
                Quaternion.Euler(0, 180, -38));
            }
            Thrown = false;
        }
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, meleeAttackRange);
    }
}
