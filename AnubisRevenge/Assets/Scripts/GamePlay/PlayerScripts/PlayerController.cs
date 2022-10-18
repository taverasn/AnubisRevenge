using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamage
{
    // Start is called before the first frame update
    [SerializeField] internal PlayerMovement pMove;
    [SerializeField] internal PlayerAnimationHandler pAnimHandler;
    [SerializeField] internal PlayerAttack pAttack;
    [SerializeField] internal PlayerInput pInput;
    [SerializeField] internal PlayerTimeManager pTime;
    [SerializeField] internal PlayerCollisions pColl;
    internal float xAxis;
    internal float yAxis;
    [SerializeField] internal int HP;

    internal Animator anim;
    internal Rigidbody2D rb;
    [SerializeField] internal LayerMask groundLayer;
    [SerializeField] internal LayerMask climbLayer;
    internal CapsuleCollider2D capCollider;
    internal BoxCollider2D boxCollider;
    internal bool isDamaged;
    internal bool gameOver;
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
    }
    
    public void takeDamage(int dmg)
    {
        HP -= dmg;
        gameManager.instance.healthBar.SetHealth(HP);
        if(HP > 0)
        {
            gameManager.instance.soundManager.hurt.Play();
        }
        else
        {
            gameManager.instance.soundManager.dead.Play();
        }
        isDamaged = true;
    }
}
