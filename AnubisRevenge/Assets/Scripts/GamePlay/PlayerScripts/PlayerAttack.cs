using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Component Variables
    private PlayerController pCtrl;

    [Header("----- Components -----")]
    [SerializeField] private LayerMask whatIsEnemies;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject dynamite;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private Transform dynamiteSpawnPoint;
    [SerializeField] private Transform attackPos;

    [Header("----- Attack Stats -----")]
    [Range(.5f, 2)] [SerializeField] private float meleeAttackRange;
    [Range(5, 100)] [SerializeField] private int damage;

    // Attack Rate Variables
    private float throwRate;
    private float shootRate;
    private float meleeRate;

    // State Variables to prevent looping
    private bool isThrowing;
    internal bool isShooting;
    internal bool isMelee;
    private bool thrown;
    [SerializeField] internal float throwDelayTimer;


    // Start is called before the first frame update
    void Start()
    {
        throwRate = 0.4f;
        pCtrl = GetComponent<PlayerController>();
    }
    // Update is called once per frame
    private void Update()
    {
        // If game over stop the user input from calling attack functions
        if(!pCtrl.gameOver)
        {
            if (thrown)
                throwDelayTimer += Time.deltaTime;
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
            pCtrl.pInput.isMeleePressed = false;
            gameManager.instance.soundManager.aud.PlayOneShot(gameManager.instance.soundManager.meleeSwing);
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, meleeAttackRange, whatIsEnemies);
            // Loops through all Game Objects that are withn Range and in the Layer Mask
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                // Removes Health from GameObjects that are within Range and in the LayerMask
                enemiesToDamage[i].GetComponent<EnemyHealth>().takeDamage(damage);
                gameManager.instance.soundManager.aud.PlayOneShot(gameManager.instance.soundManager.meleeHit);
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
            pCtrl.pInput.isThrowPressed = false;
            thrown = true;
        }
        // Thrown true? not currently throwing?
        // Also checks if throwdelay timer is greater than throw rate to cause the object to spawn at the right time during the animation
        if (thrown && !isThrowing && throwDelayTimer >= throwRate)
        {
            PlayerPrefs.SetInt("dynamite", PlayerPrefs.GetInt("dynamite") - 1);
            gameManager.instance.soundManager.aud.PlayOneShot(gameManager.instance.soundManager.dynamiteThrow);
            gameManager.instance.limitedProjectile.UseDynamite();
            isThrowing = true;
            thrown = false;
            throwDelayTimer = 0;
            // Spawn Object at set position and rotation
            Instantiate(dynamite, dynamiteSpawnPoint.transform.position, dynamite.transform.rotation);
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
            pCtrl.pInput.isShootPressed = false;
            shootRate = pCtrl.anim.GetCurrentAnimatorStateInfo(0).length;
            // Spawn Object at set position and rotation
            gameManager.instance.soundManager.aud.PlayOneShot(gameManager.instance.soundManager.shoot);
            Instantiate(bullet ,bulletSpawnPoint.transform.position, transform.rotation);
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
