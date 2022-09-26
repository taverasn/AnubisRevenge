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

    private bool thrown;
    [SerializeField] private float throwDelay;

    // Start is called before the first frame update
    void Start()
    {
        pTime = GetComponent<PlayerTimeManager>();
        pInput = GetComponent<PlayerInput>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        // Melee Button Pressed?
        if (pInput.GetisMeleePressed())
        {
            // Checks if there are any gameObjects in the whatIsEnemies LayerMask and if they are within Range
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, meleeAttackRange, whatIsEnemies);
            // Loops through all Game Objects that are withn Range and in the Layer Mask
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                // Removes Health from GameObjects that are within Range and in the LayerMask
                enemiesToDamage[i].GetComponent<Health>().TakeDamage(damage);
            }
        }
        // Shoot Button Pressed?
        if (pInput.GetisShootPressed())
        {  
            // Spawns Bullet Game Object at set position and rotation
            Instantiate(projectilePrefab, projectileSpawnPoint.transform.position, transform.rotation);
        }
        // Throw Button Pressed
        if (pInput.GetisThrowPressed())
        {
            thrown = true;
            // Spawns Bullet Game Object at set position and rotation
        }
        if(thrown && pTime.GetThrowDelayTimer() >= throwDelay)
        {
            thrown = false;
            pTime.SetThrowDelayTimer(0);
            Instantiate(launchableProjectilePrefab, launchableProjectileSpawnPoint.transform.position, launchableProjectilePrefab.transform.rotation);
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
