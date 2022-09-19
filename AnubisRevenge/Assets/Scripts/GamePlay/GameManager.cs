using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int maxHealth = 100;
    public int currentHealth;
    private Animator animator;
    public GameObject player;
    //public HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        //healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddHealth(int value)
    {
        currentHealth += value;
        //healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            Debug.Log("GG");
            currentHealth = 0;
        }
        Debug.Log("Health: " + currentHealth);
    }

    public int GetHealth()
    {
        return currentHealth;
    }

}
