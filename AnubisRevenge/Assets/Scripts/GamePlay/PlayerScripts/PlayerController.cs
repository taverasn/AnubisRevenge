using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] internal PlayerMovement pMove;
    [SerializeField] internal PlayerAnimationHandler pAnimHandler;
    [SerializeField] internal PlayerAttack pAttack;
    [SerializeField] internal PlayerInput pInput;
    [SerializeField] internal PlayerTimeManager pTime;
    [SerializeField] internal PlayerCollisions pColl;
    [SerializeField] internal Health pHealth;

    internal Animator anim;
    internal Rigidbody2D rb;
    [SerializeField] internal LayerMask groundLayer;
    internal CapsuleCollider2D capCollider;
    internal BoxCollider2D boxCollider;

    internal bool gameOver;
    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        capCollider = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
}
