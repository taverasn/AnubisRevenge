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
        pCtrl = GetComponent<PlayerController>();
        
        if(gameObject.tag == "Dynamite")
        {
            direction = transform.right + (Vector3.up * 2);
            GetComponent<Rigidbody2D>().AddForce(direction * speed, ForceMode2D.Impulse);
        }
        transform.Translate(LaunchOffset);

        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if(gameObject.tag == "Bullet")
        {
            movementSpeed = -speed * Time.deltaTime;
            transform.Translate(movementSpeed, 0, 0);
            if (anim.GetBool("explode") == true)
            Destroy(gameObject, 5);
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
            Destroy(gameObject);
        }
    }
}