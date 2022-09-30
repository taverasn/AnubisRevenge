using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    private PlayerController pCtrl;
    // Start is called before the first frame update
    void Start()
    {
        pCtrl = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public bool isClimbing()
    {
        RaycastHit2D hit = Physics2D.BoxCast(pCtrl.capCollider.bounds.center, pCtrl.capCollider.bounds.size,
            0, Vector2.one, 0.1f, pCtrl.climbLayer);
        return hit.collider != null;
    }
    public bool isGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(pCtrl.boxCollider.bounds.center, pCtrl.boxCollider.bounds.size,
            0, Vector2.down, 0.1f, pCtrl.groundLayer);
        return hit.collider != null;
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
