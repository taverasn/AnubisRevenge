using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectCollisions : MonoBehaviour
{
    private GameManager gameManager;
    private Animator animator;
    private Health playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        animator = GameObject.Find("PlayerCharacter").GetComponent<Animator>();
        playerHealth = GameObject.Find("PlayerCharacter").GetComponent<Health>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && gameObject.tag == "Obstacle")
        {
            if (playerHealth.currentHealth > 0)
            {
                playerHealth.TakeDamage(50);
            }
        }
    }


}
