using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnubisAttacks : MonoBehaviour
{
    private PlayerController PC;
    AnubisMovement moving;
    Health playerHealth;
    BossHealth bosshealth;
    public Animator animator;
    private string currentState;

    const string player = "PlayerCharacter";
    const string ANUBIS_IDLE = "Anubis_Idle";
    const string ANUBIS_SLASH = "Anubis_Slash";
    const string ANUBIS_RUNSLASH = "Anubis_RunSlash";

    public float delay;

    public Transform circleOrigin;
    public float radius;
    public bool onCoolDown;
    [SerializeField] private int damage;

    void Start()
    {
        PC = GameObject.Find(player).GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        moving = GetComponent<AnubisMovement>();
        bosshealth = GetComponent<BossHealth>();
    }
    
    void Update()
    {
        if (!bosshealth.dead)
            DetectPlayer();
    }

    void ChangeAnimationState(string newState)
    {
        // stop same animation from interrupting itself
        if (currentState == newState) return;

        // play the animation
        animator.Play(newState);

        // reassign the current state
        currentState = newState;
    }

    public void attack()
    {
        if (onCoolDown)
        {
            ChangeAnimationState(ANUBIS_SLASH);
            PC.takeDamage(damage);
            Collider2D[] PlayerToDamage = Physics2D.OverlapCircleAll(circleOrigin.position, radius);
            return;
        }
        onCoolDown = true;
        StartCoroutine(delayAttack());
    }

    private IEnumerator delayAttack()
    {
        ChangeAnimationState(ANUBIS_IDLE);
        yield return new WaitForSeconds(delay);
        onCoolDown = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 position = circleOrigin == null ? Vector3.zero : circleOrigin.position;
        Gizmos.DrawWireSphere(position, radius);
    }

    public void DetectPlayer()
    {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(circleOrigin.position, radius))
        {
            if (collider.name == player)
            {
                moving.speed = 0;
                attack();
            }
        }
    }
}