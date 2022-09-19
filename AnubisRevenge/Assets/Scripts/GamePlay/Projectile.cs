using UnityEngine;
using UnityEngine.UI;
public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private float lifetime;

    public int damage;

    private Animator anim;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > 5)
            gameObject.SetActive(false);

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<Health>().TakeDamage(damage);
            anim.SetTrigger("explode");
            Destroy(gameObject);
        }
    }
/*    public void SetDirection(float _direction)
    {
        lifetime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }*/
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

}