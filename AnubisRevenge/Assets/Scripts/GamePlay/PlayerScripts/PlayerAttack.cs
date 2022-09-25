using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerInput pInput;

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject launchableProjectilePrefab;

    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private Transform launchableProjectileSpawnPoint;
    [SerializeField] private Transform attackPos;

    [SerializeField] private LayerMask whatIsEnemies;

    [SerializeField] private float meleeAttackRange;
    [SerializeField] private float damage;

    // Start is called before the first frame update
    void Start()
    {
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
            // Spawns Bullet Game Object at set position and rotation
            Instantiate(launchableProjectilePrefab, launchableProjectileSpawnPoint.position, transform.rotation);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, meleeAttackRange);
    }
}
