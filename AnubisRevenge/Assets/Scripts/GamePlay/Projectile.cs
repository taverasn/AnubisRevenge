using UnityEngine;
using UnityEngine.UI;
public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    public Vector3 LaunchOffset;
    public int damage;
    private float movementSpeed;
    public PlayerController pCtrl;
    private Rigidbody2D rb;
    private Vector3 direction;
    private void Start()
    {
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
            GetComponent<Rigidbody2D>().AddForce(direction * speed, ForceMode2D.Impulse);
        }

    }
    private void Update()
    {
        if(gameObject.tag == "Bullet")
        {
            if(pCtrl.facingRight)
                movementSpeed = speed * Time.deltaTime;
            else
                movementSpeed = -speed * Time.deltaTime;
            transform.Translate(movementSpeed, 0, 0);
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