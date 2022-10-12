using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Component Variables
    private PlayerController pCtrl;

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
    [SerializeField] private int damage;

    // Attack Rate Variables
    [SerializeField] private float throwRate;
    [SerializeField] private float shootRate;
    [SerializeField] private float meleeRate;

    // State Variables to prevent looping
    private bool isThrowing;
    internal bool isShooting;
    internal bool isMelee;
    private bool thrown;

    // Start is called before the first frame update
    void Start()
    {
        pCtrl = GetComponent<PlayerController>();
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
        if(pCtrl.pInput.isMeleePressed && !isMelee)
        {
            isMelee = true;
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, meleeAttackRange, whatIsEnemies);
            // Loops through all Game Objects that are withn Range and in the Layer Mask
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                // Removes Health from GameObjects that are within Range and in the LayerMask
                enemiesToDamage[i].GetComponent<EnemyHealth>().takeDamage(damage);
            }
            yield return new WaitForSeconds(meleeRate);
            isMelee = false;
        }
    }

    IEnumerator dynamiteThrow()
    {
        // Throw Pressed?
        if (pCtrl.pInput.isThrowPressed)
        {
            thrown = true;
        }
        // Thrown true? not currently throwing?
        // Also checks if throwdelay timer is greater than throw rate to cause the object to spawn at the right time during the animation
        if (thrown && !isThrowing && pCtrl.pTime.throwDelayTimer >= throwRate)
        {
            gameManager.instance.soundManager.dynamiteThrow.Play();
            gameManager.instance.limitedProjectile.UseDynamite();
            isThrowing = true;
            thrown = false;
            pCtrl.pTime.throwDelayTimer = 0;
            // Spawn Object at set position and rotation
            Instantiate(launchableProjectilePrefab, launchableProjectileSpawnPoint.transform.position, launchableProjectilePrefab.transform.rotation);
            yield return new WaitForSeconds(throwRate);
            gameManager.instance.pCtrl.pInput.throwMultiplierTimer = 0;
            isThrowing = false;
        }
    }

    IEnumerator shoot()
    {
        // Shoot Pressed? and not currently shooting
        if(pCtrl.pInput.isShootPressed && !isShooting)
        {
            isShooting = true;
            shootRate = pCtrl.anim.GetCurrentAnimatorStateInfo(0).length;
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
