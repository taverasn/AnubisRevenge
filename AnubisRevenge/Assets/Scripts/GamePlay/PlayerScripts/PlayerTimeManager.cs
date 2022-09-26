using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTimeManager : MonoBehaviour
{
    // Components
    private PlayerAttack pAttack;

    // Timer Variables
    private float timeBtwMeleeAttack;
    private float timeBtwRangeAttack;
    private float timeBtwThrowAttack;
    [SerializeField] private float throwDelayTimer;
    // Time Delay Variables
    [SerializeField] private float startTimeBtwMeleeAttack;    
    [SerializeField] private float startTimeBtwRangeAttack;
    [SerializeField] private float startTimeBtwThrowAttack;

    private void Start()
    {
        pAttack = GetComponent<PlayerAttack>();
    }

    private void FixedUpdate()
    {
        // Adding time in seconds to timer variables
        timeBtwMeleeAttack += Time.deltaTime;
        timeBtwRangeAttack += Time.deltaTime;
        timeBtwThrowAttack += Time.deltaTime;
        if (pAttack.GetThrown())
            throwDelayTimer += Time.deltaTime;
    }
    // Throw Delay Getters/Setters
    public float GetThrowDelayTimer()
    {
        return throwDelayTimer;
    }
    public void SetThrowDelayTimer(float _throwDelayTimer)
    {
        throwDelayTimer = _throwDelayTimer;
    }

    // Melee Attack Timer Getters/Setters
    public float GetTimeBtwMeleeAttack()
    {
        return timeBtwMeleeAttack;
    }    
    public float GetStartTimeBtwMeleeAttack()
    {
        return startTimeBtwMeleeAttack;
    }    
    public void SetTimeBtwMeleeAttack(float _timeBtwMeleeAttack)
    { 
        timeBtwMeleeAttack = _timeBtwMeleeAttack;
    }
    // Range Attack Timer Getters/Setters
    public float GetTimeBtwRangeAttack()
    {
        return timeBtwRangeAttack;
    }    
    public float GetStartTimeBtwRangeAttack()
    {
        return startTimeBtwRangeAttack;
    }    
    public void SetTimeBtwRangeAttack(float _timeBtwRangeAttack)
    {
        timeBtwRangeAttack = _timeBtwRangeAttack;
    }
    // Throw Attack Timer Getters/Setters
    public float GetTimeBtwThrowAttack()
    {
        return timeBtwThrowAttack;
    }    
    public float GetStartTimeBtwThrowAttack()
    {
        return startTimeBtwThrowAttack;
    }    
    public void SetTimeBtwThrowAttack(float _timeBtwThrowAttack)
    {
        timeBtwThrowAttack = _timeBtwThrowAttack;
    }

}
