using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamage
{
    [Header("----- Components -----")]
    [SerializeField] internal PlayerMovement pMove;
    [SerializeField] internal PlayerAnimationHandler pAnimHandler;
    [SerializeField] internal PlayerAttack pAttack;
    [SerializeField] internal PlayerInput pInput;
    [SerializeField] internal PlayerCollisions pColl;
    [SerializeField] internal LayerMask groundLayer;
    [SerializeField] internal LayerMask climbLayer;
    internal float xAxis;
    internal float yAxis;

    [Header("----- Player Stat -----")]
    [SerializeField] internal int HP;

    internal Animator anim;
    internal Rigidbody2D rb;
    internal CapsuleCollider2D capCollider;
    internal BoxCollider2D boxCollider;
    internal bool isDamaged;
    internal bool gameOver;
    bool playingSteps;
    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        capCollider = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        gameManager.instance.healthBar.SetMaxHealth(HP);
    }
    private void Update()
    {
        xAxis = Input.GetAxisRaw("Horizontal") * .1f;
        yAxis = Input.GetAxisRaw("Vertical") * .1f;
        StartCoroutine(PlaySteps());
    }
    
    IEnumerator PlaySteps()
    {
        if (xAxis != 0 && pColl.isGrounded() && !playingSteps)
        {
            playingSteps = true;
            gameManager.instance.soundManager.aud.PlayOneShot(gameManager.instance.soundManager.walk);
            if (pInput.isRunning)
                yield return new WaitForSeconds(0.3f);
            else
                yield return new WaitForSeconds(0.5f);
            playingSteps = false;
            gameManager.instance.soundManager.aud.Stop();
        }
    }

    public void takeDamage(int dmg)
    {
        HP -= dmg;
        gameManager.instance.healthBar.SetHealth(HP);
        if(HP > 0)
        {
            gameManager.instance.soundManager.aud.PlayOneShot(gameManager.instance.soundManager.hurt);
        }
        else
        {
            gameManager.instance.soundManager.aud.PlayOneShot(gameManager.instance.soundManager.dead);
        }
        isDamaged = true;
    }
}
