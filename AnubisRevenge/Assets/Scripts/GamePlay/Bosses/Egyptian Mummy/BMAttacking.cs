using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BMAttacking : MonoBehaviour
{
    BMMovement moving;
    Health playerHealth;
   
    public Animator animator;
    private string currentState;
    
    const string player = "PlayerCharacter";
    const string BM_IDLE = "BM_Idle";
    const string BM_WALK = "BM_Walk";
    const string BM_ATTACK = "BM_Attack";

    public float delay;

    public Transform circleOrigin;
    public float radius;
    public bool onCoolDown;
    [SerializeField] private int damage;

   
    void Start()
    {
        animator = GetComponent<Animator>();
        moving = GetComponent<BMMovement>();
        playerHealth = GameObject.Find("PlayerCharacter").GetComponent<Health>();
    }

    void Update()
    {
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
            ChangeAnimationState(BM_ATTACK);
            Collider2D[] PlayerToDamage = Physics2D.OverlapCircleAll(circleOrigin.position, radius);
            return;
        }
        onCoolDown = true;
        StartCoroutine(delayAttack());
    }

    private IEnumerator delayAttack()
    {
        ChangeAnimationState(BM_IDLE);
        yield return new WaitForSeconds(delay);
        onCoolDown = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
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
