using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectCollisions : MonoBehaviour
{
    private Health playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.Find("PlayerCharacter").GetComponent<Health>();
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
