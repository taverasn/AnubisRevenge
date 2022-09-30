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

    [SerializeField] internal Animator anim;
    [SerializeField] internal Rigidbody2D rb;

    internal bool gameOver;
    void Awake()
    {
        Debug.Log("Controller");
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
}
