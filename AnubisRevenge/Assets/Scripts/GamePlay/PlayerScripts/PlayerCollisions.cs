using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    private PlayerController pCtrl;
    internal bool canClimb;
    // Start is called before the first frame update
    void Start()
    {
        pCtrl = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public bool isGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(pCtrl.boxCollider.bounds.center, pCtrl.boxCollider.bounds.size,
            0, Vector2.down, 0.1f, pCtrl.groundLayer);
        return hit.collider != null;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
/*        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        if (other.gameObject.tag == "Ladder")
        {
            canClimb = true;
            pCtrl.rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            pCtrl.rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            canClimb = false;
            pCtrl.rb.constraints = RigidbodyConstraints2D.None;
            pCtrl.rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }*/
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            if (pCtrl.pHealth.currentHealth > 0)
            {
                pCtrl.pHealth.TakeDamage(pCtrl.pHealth.currentHealth);
            }
        }
    }
}
