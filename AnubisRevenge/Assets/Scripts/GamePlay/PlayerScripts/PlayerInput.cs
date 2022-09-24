using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private bool isRunning;
    [SerializeField] private bool isCrouching;
    [SerializeField] private bool isIdle;
    [SerializeField] private bool isWalking;
    private float xAxis;
    private float yAxis;

    private PlayerController pCtrl;
    private PlayerAttack pAttack;
    // Start is called before the first frame update
    void Start()
    {
        pCtrl = GetComponent<PlayerController>();
        pAttack = GetComponent<PlayerAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        xAxis = Input.GetAxis("Horizontal");
        yAxis = Input.GetAxis("Vertical");
        // Sprint Key Pressed
        if (Input.GetButtonDown("Sprint"))
        {
            isRunning = true;
        }

        // Sprint Key Released
        if (Input.GetButtonUp("Sprint"))
        {
            isRunning = false;
        }
        // Crouch Key Pressed?
        if (Input.GetButtonDown("Crouch"))
        {
            isCrouching = true;
        }
        if (Input.GetButtonUp("Crouch"))
        {
            isCrouching = false;
        }
    }

    private void FixedUpdate()
    {
        if (pCtrl.GetisGrounded() && !pAttack.isMelee && !pAttack.isShooting && !pAttack.isThrowing)
        {
            if (xAxis != 0)
            {
                isIdle = false;
                if (GetisRunning())
                {
                    isWalking = false;
                }
                else
                {
                    isWalking = true;
                }
            }
            else
            {
                isWalking = false;
                isIdle = true;
            }
        }
    }

    public bool GetisCrouching()
    {
        return isCrouching;
    }
    public bool GetisRunning()
    {
        return isRunning;
    }
    public bool GetisIdle()
    {
        return isIdle;
    }
    public bool GetisWalking()
    {
        return isWalking;
    }
}
