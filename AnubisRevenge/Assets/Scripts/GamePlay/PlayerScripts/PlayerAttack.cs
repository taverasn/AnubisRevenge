using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerInput pInput;
    private PlayerAnimationHandler pAnimHandler;
    public GameObject projectilePrefab;
    public GameObject launchableProjectilePrefab;
    public Animator animator;
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
    private Animator anim;
    private GameObject player;

    private bool isMeleePressed;
    public bool isMelee;    
    private bool isShootPressed;
    public bool isShooting;    
    private bool isThrowPressed;
    public bool isThrowing;

    private float attackDelay;
    // Animation States
    private const string PLAYER_CROUCHSHOOT = "Player_CrouchShoot";
    private const string PLAYER_CROUCHTHROW = "Player_CrouchThrow";
    private const string PLAYER_CROUCHMELEE = "Player_CrouchMelee";
    private const string PLAYER_MELEE = "Player_Melee";
    private const string PLAYER_SHOOT = "Player_Shoot";
    private const string PLAYER_THROW = "Player_Throw";
    private const string PLAYER_JUMPSHOOT = "Player_JumpShoot";
    private const string PLAYER_JUMPTHROW = "Player_JumpThrow";
    private const string PLAYER_JUMPMELEE = "Player_JumpMelee";
    private const string PLAYER_WALKSHOOT = "Player_WalkShoot";
    private const string PLAYER_RUNSHOOT = "Player_RunShoot";

    // Start is called before the first frame update
    void Start()
    {
        pInput = GetComponent<PlayerInput>();
        pAnimHandler = GetComponent<PlayerAnimationHandler>();
        player = GameObject.Find("PlayerCharacter");
        pCtrl = GameObject.Find("PlayerCharacter").GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && !pInput.GetisRunning() && !pInput.GetisWalking())
        {
            isMeleePressed = true;
        }
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            isShootPressed = true;
        }
        if(Input.GetKeyDown(KeyCode.F) && !pInput.GetisRunning() && !pInput.GetisWalking())
        {
            isThrowPressed = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Melee

        if (isMeleePressed && timeBtwMeleeAttack > startTimeMeleeBtwAttack && !pInput.GetisRunning() && !pInput.GetisWalking())
        {
            isMeleePressed = false;

            if (!isMelee)
            {
                isMelee = true;
                timeBtwMeleeAttack = 0;
                if (pInput.GetisCrouching())
                {
                    pAnimHandler.ChangeAnimationState(PLAYER_CROUCHMELEE);
                }
                else if (!pCtrl.GetisGrounded())
                {
                    pAnimHandler.ChangeAnimationState(PLAYER_JUMPMELEE);
                }
                else if (pInput.GetisIdle())
                {
                    pAnimHandler.ChangeAnimationState(PLAYER_MELEE);
                }
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, meleeAttackRange, whatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<Health>().TakeDamage(damage);
                }
            }

            attackDelay = anim.GetCurrentAnimatorStateInfo(0).length;
            Invoke("MeleeAttackComplete", attackDelay);
        }
        timeBtwMeleeAttack += Time.deltaTime;



        // Shoot
        if (isShootPressed && timeBtwRangeAttack > startTimeRangeBtwAttack)
        {
            isShootPressed = false;
            if(!isShooting)
            {
                isShooting = true;

                timeBtwRangeAttack = 0;
                if (pInput.GetisCrouching())
                {
                    pAnimHandler.ChangeAnimationState(PLAYER_CROUCHSHOOT);
                }
                else if (!pCtrl.GetisGrounded())
                {
                    pAnimHandler.ChangeAnimationState(PLAYER_JUMPSHOOT);
                }
                else if (pInput.GetisIdle())
                {
                    pAnimHandler.ChangeAnimationState(PLAYER_SHOOT);
                }
                else if (pInput.GetisWalking())
                {
                    pAnimHandler.ChangeAnimationState(PLAYER_WALKSHOOT);
                }
                else if (pInput.GetisRunning())
                {
                    pAnimHandler.ChangeAnimationState(PLAYER_RUNSHOOT);
                }
                Instantiate(projectilePrefab, projectileSpawnPoint.transform.position, player.transform.rotation);
            }

            attackDelay = anim.GetCurrentAnimatorStateInfo(0).length;
            Invoke("ShootAttackComplete", attackDelay);

        }
        timeBtwRangeAttack += Time.deltaTime;

        // Throw
        if (isThrowPressed && timeBtwThrowAttack > startTimeThrowBtwAttack && !pInput.GetisRunning() && !pInput.GetisWalking())
        {
            isThrowPressed = false;
            if(!isThrowing)
            {
                isThrowing = true;
                timeBtwThrowAttack = 0;
                if (pInput.GetisCrouching())
                {
                    pAnimHandler.ChangeAnimationState(PLAYER_CROUCHTHROW);
                }
                else if (!pCtrl.GetisGrounded())
                {
                    pAnimHandler.ChangeAnimationState(PLAYER_JUMPTHROW);
                }
                else if (pInput.GetisIdle())
                {
                    pAnimHandler.ChangeAnimationState(PLAYER_THROW);
                }
                Thrown = true;
                throwDelay = 0;

                attackDelay = anim.GetCurrentAnimatorStateInfo(0).length;
                Invoke("ThrowAttackComplete", attackDelay);
            }
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

    void MeleeAttackComplete()
    {
        isMelee = false;
    }
    void ShootAttackComplete()
    {
        isShooting = false;
    }
    void ThrowAttackComplete()
    {
        isThrowing = false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, meleeAttackRange);
    }
}
