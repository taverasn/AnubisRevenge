using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Component Variables
    private PlayerInput pInput;
    private PlayerTimeManager pTime;
    private PlayerController pCtrl;
    private Animator anim;

    // Prefab Variables
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject launchableProjectilePrefab;

    // Attack Position Variables
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private Transform launchableProjectileSpawnPoint;
    [SerializeField] private Transform attackPos;

    // Layermask Variable
    [SerializeField] private LayerMask whatIsEnemies;

    // Melee Range Variable
    [SerializeField] private float meleeAttackRange;

    // Damage Variable
    [SerializeField] private float damage;

    // Attack Rate Variables
    [SerializeField] private float throwRate;
    [SerializeField] private float shootRate;
    [SerializeField] private float meleeRate;

    // State Variables to prevent looping
    private bool isThrowing;
    private bool isShooting;
    private bool isMelee;
    private bool thrown;

    // Start is called before the first frame update
    void Start()
    {
        pCtrl = GetComponent<PlayerController>();
        pTime = GetComponent<PlayerTimeManager>();
        pInput = GetComponent<PlayerInput>();
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    private void Update()
    {
        // If game over stop the user input from calling attack functions
        if(!pCtrl.gameOver)
        {
            StartCoroutine(shoot());
            StartCoroutine(dynamiteThrow());
            StartCoroutine(melee());
        }
    }

    // Attack Functions
    IEnumerator melee()
    {
        // Melee Pressed? and not currently in melee
        if(pInput.GetisMeleePressed() && !isMelee)
        {
            isMelee = true;
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, meleeAttackRange, whatIsEnemies);
            // Loops through all Game Objects that are withn Range and in the Layer Mask
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                // Removes Health from GameObjects that are within Range and in the LayerMask
                enemiesToDamage[i].GetComponent<Health>().TakeDamage(damage);
            }
            yield return new WaitForSeconds(meleeRate);
            isMelee = false;
        }
    }

    IEnumerator dynamiteThrow()
    {
        // Throw Pressed?
        if (pInput.GetisThrowPressed())
        {
            thrown = true;
        }
        // Thrown true? not currently throwing?
        // Also checks if throwdelay timer is greater than throw rate to cause the object to spawn at the right time during the animation
        if (thrown && !isThrowing && pTime.GetThrowDelayTimer() >= throwRate)
        {
            isThrowing = true;
            thrown = false;
            pTime.SetThrowDelayTimer(0);
            // Spawn Object at set position and rotation
            Instantiate(launchableProjectilePrefab, launchableProjectileSpawnPoint.transform.position, launchableProjectilePrefab.transform.rotation);
            yield return new WaitForSeconds(throwRate);
            isThrowing = false;
        }
    }

    IEnumerator shoot()
    {
        // Shoot Pressed? and not currently shooting
        if(pInput.GetisShootPressed() && !isShooting)
        {
            isShooting = true;
            shootRate = anim.GetCurrentAnimatorStateInfo(0).length;
            // Spawn Object at set position and rotation
            Instantiate(projectilePrefab, projectileSpawnPoint.transform.position, transform.rotation);
            yield return new WaitForSeconds(shootRate);
            isShooting = false;

        }
    }

    public bool GetThrown()
    {
        return thrown;
    }

    // Draws the players melee attack range in the editor using the attackPos game object to get the position and the melee Range as a float to decide how big the circle will be
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, meleeAttackRange);
    }
}
