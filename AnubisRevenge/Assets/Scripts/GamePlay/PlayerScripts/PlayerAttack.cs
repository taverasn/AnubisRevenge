using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerInput pInput;
    private PlayerTimeManager pTime;

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject launchableProjectilePrefab;

    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private Transform launchableProjectileSpawnPoint;
    [SerializeField] private Transform attackPos;

    [SerializeField] private LayerMask whatIsEnemies;

    [SerializeField] private float meleeAttackRange;
    [SerializeField] private float damage;

    private bool isThrowing;
    [SerializeField] private float throwRate;
    private bool isShooting;
    [SerializeField] private float shootRate;
    [SerializeField] private float meleeRate;
    private bool isMelee;
    private Animator anim;
    private bool thrown;

    // Start is called before the first frame update
    void Start()
    {
        pTime = GetComponent<PlayerTimeManager>();
        pInput = GetComponent<PlayerInput>();
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    private void Update()
    {
        StartCoroutine(shoot());
        StartCoroutine(dynamiteThrow());
        StartCoroutine(melee());
    }
    IEnumerator melee()
    {
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
        if (pInput.GetisThrowPressed())
        {
            thrown = true;
            // Spawns Bullet Game Object at set position and rotation
        }
        if (thrown && !isThrowing && pTime.GetThrowDelayTimer() >= throwRate)
        {
            isThrowing = true;
            thrown = false;
            pTime.SetThrowDelayTimer(0);
            Instantiate(launchableProjectilePrefab, launchableProjectileSpawnPoint.transform.position, transform.rotation);
            yield return new WaitForSeconds(throwRate);
            isThrowing = false;
        }
    }

    IEnumerator shoot()
    {
        if(pInput.GetisShootPressed() && !isShooting)
        {
            isShooting = true;
            shootRate = anim.GetCurrentAnimatorStateInfo(0).length;
            Instantiate(projectilePrefab, projectileSpawnPoint.transform.position, transform.rotation);
            yield return new WaitForSeconds(shootRate);
            isShooting = false;

        }
    }

    public bool GetThrown()
    {
        return thrown;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, meleeAttackRange);
    }
}
