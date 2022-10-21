using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    private PlayerController pCtrl;
    private float xAxis;
    // Start is called before the first frame update
    void Start()
    {
        pCtrl = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        xAxis = Input.GetAxis("Horizontal");
    }

    public bool isClimbing()
    {
        RaycastHit2D hit = Physics2D.BoxCast(pCtrl.capCollider.bounds.center, pCtrl.capCollider.bounds.size,
            0, Vector2.one, 0.1f, pCtrl.climbLayer);
        if(pCtrl.pInput.isClimbing && hit && xAxis == 0)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(hit.transform.position.x, transform.position.y, transform.position.z), pCtrl.pMove.horizontalClimbSpeed * Time.deltaTime);
        }
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
            if (pCtrl.HP > 0)
            {
                pCtrl.takeDamage(pCtrl.HP);
            }
        }
    }
}
