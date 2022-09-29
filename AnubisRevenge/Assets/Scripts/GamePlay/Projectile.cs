using UnityEngine;
using UnityEngine.UI;
public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    public Vector3 LaunchOffset;
    public int damage;
    private float movementSpeed;
    public PlayerController pCtrl;
    private Animator anim;
    private Rigidbody2D rb;
    private Vector3 direction;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        pCtrl = GameObject.Find("PlayerCharacter").GetComponent<PlayerController>();
        if(gameObject.tag == "Dynamite")
        {
            if(pCtrl.facingRight)
            {
                transform.rotation = Quaternion.Euler(0, 0, -37.66f);
                direction = transform.right + (Vector3.up * 1.5f);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 37.66f);
                direction = -transform.right + (Vector3.up * 1.5f);
            }
            rb.AddForce(direction * speed, ForceMode2D.Impulse);
        }
        if(gameObject.tag == "Bullet")
        {
            if(pCtrl.facingRight)
                movementSpeed = speed;
            else
                movementSpeed = -speed;
            rb.velocity = new Vector2(movementSpeed, 0);
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<Health>().TakeDamage(damage);
        }
        if(other.gameObject.tag != "Player")
        {
            anim.SetBool("explode", true);
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            Destroy(gameObject, anim.GetCurrentAnimatorStateInfo(0).length);
        }
    }
}