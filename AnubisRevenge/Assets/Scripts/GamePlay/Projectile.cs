using UnityEngine;
using UnityEngine.UI;
public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private float lifetime;
    public Vector3 LaunchOffset;
    public bool thrown;
    public int damage;
    private float movementSpeed;
    public PlayerController pCtrl;

    private Animator anim;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        pCtrl = GameObject.Find("PlayerCharacter").GetComponent<PlayerController>();
    }
    private void Awake()
    {
    }
    private void Update()
    {
        if(gameObject.tag == "Dynamite")
        {
            Vector3 direction = transform.right + new Vector3(2, 2, 0);
            if(pCtrl.facingRight)
            {
                rb.AddForce(direction * speed , ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(direction * -speed, ForceMode2D.Impulse);
            }
            transform.Translate(LaunchOffset);
        }
        if(gameObject.tag == "Bullet")
        {
            if(pCtrl.facingRight)
            {
                movementSpeed = speed * Time.deltaTime;
            }
            else
            {
                movementSpeed = -speed * Time.deltaTime;
            }
            transform.Translate(movementSpeed, 0, 0);

            lifetime += Time.deltaTime;
            if (lifetime > 5 || anim.GetBool("explode") == true)
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<Health>().TakeDamage(damage);
            anim.SetBool("explode", true);
        }
        else if (other.gameObject.tag != "Player")
        {
            anim.SetBool("explode", true);

        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<Health>().TakeDamage(damage);
        }
    }

}