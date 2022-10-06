using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTimeManager : MonoBehaviour
{
    // Components
    private PlayerController pCtrl;

    // Timer Variables
    internal float timeBtwMeleeAttack;
    internal float timeBtwRangeAttack;
    internal float timeBtwThrowAttack;
    [SerializeField] internal float throwDelayTimer;
    // Time Delay Variables
    [SerializeField] internal float startTimeBtwMeleeAttack;    
    [SerializeField] internal float startTimeBtwRangeAttack;
    [SerializeField] internal float startTimeBtwThrowAttack;

    private void Start()
    {
        pCtrl = GetComponent<PlayerController>();
    }

    private void FixedUpdate()
    {
        // Adding time in seconds to timer variables
        timeBtwMeleeAttack += Time.deltaTime;
        timeBtwRangeAttack += Time.deltaTime;
        timeBtwThrowAttack += Time.deltaTime;
        if (pCtrl.pAttack.GetThrown())
            throwDelayTimer += Time.deltaTime;
    }
}
