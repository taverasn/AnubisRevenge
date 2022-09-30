using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    private PlayerController pCtrl;
    internal bool isGrounded;
    // Start is called before the first frame update
    void Start()
    {
        pCtrl = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Obstacle" || other.gameObject.tag == "Enemy")
        {
            if (pCtrl.pHealth.currentHealth > 0)
            {
                pCtrl.pHealth.TakeDamage(50);
            }
        }
    }
}
