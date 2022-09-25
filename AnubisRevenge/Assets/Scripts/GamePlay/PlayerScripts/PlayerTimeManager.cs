using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTimeManager : MonoBehaviour
{
    // Timer Variables
    private float timeBtwMeleeAttack;
    private float timeBtwRangeAttack;
    private float timeBtwThrowAttack;
    // Time Delay Variables
    [SerializeField] private float startTimeBtwMeleeAttack;    
    [SerializeField] private float startTimeBtwRangeAttack;
    [SerializeField] private float startTimeBtwThrowAttack;

    private void FixedUpdate()
    {
        // Adding time in seconds to timer variables
        timeBtwMeleeAttack += Time.deltaTime;
        timeBtwRangeAttack += Time.deltaTime;
        timeBtwThrowAttack += Time.deltaTime;
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
