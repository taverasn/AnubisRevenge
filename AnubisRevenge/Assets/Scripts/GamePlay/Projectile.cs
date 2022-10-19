using UnityEngine;
using UnityEngine.UI;
public class Projectile : MonoBehaviour
{
    // Component Variables
    private Animator anim;
    private Rigidbody2D rb;
    // Speed and Direction Variables
    [SerializeField] private float speed;
    private float movementSpeed;
    private Vector3 direction;
    [SerializeField] float arcMultiplier;

    // Damage Variable
    public int damage;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        // Tag Dynamite?
        // Causes Dynamite to move in a parabola depnding on direction player is facing
        if(gameObject.tag == "Dynamite")
        {
            if(gameManager.instance.pCtrl.pMove.facingRight)
            {
                // Set Dynamite Rotation
                transform.rotation = Quaternion.Euler(0, 0, -37.66f);
                direction = transform.right + ((Vector3.up * arcMultiplier) * (1.5f * gameManager.instance.pCtrl.pInput.throwMultiplierTimer));
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 37.66f);
                direction = -transform.right + ((Vector3.up * arcMultiplier) * (1.5f * gameManager.instance.pCtrl.pInput.throwMultiplierTimer));
            }
            gameManager.instance.soundManager.aud.PlayOneShot(gameManager.instance.soundManager.dynamite);
            gameManager.instance.soundManager.aud.loop = true;
            rb.AddForce(direction * speed, ForceMode2D.Impulse);
        }
        // Tag Bullet?
        // Causes bullet to move in a straight line depending on direction player is facing
        if(gameObject.tag == "Bullet")
        {
            if(gameManager.instance.pCtrl.pMove.facingRight)
                movementSpeed = speed;
            else
                movementSpeed = -speed;
            rb.velocity = new Vector2(movementSpeed, 0);
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Deal damage if the game object is an Enemy
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyHealth>().takeDamage(damage);
        }
        else if(other.gameObject.tag == "Anubis")
        {
            other.gameObject.GetComponent<BossHealth>().TakeDamage(damage);
        }
        // If the game object is not the player use the explode animation and freeze the gameobject
        // and destroy it after the animation has played
        if(other.gameObject.tag != "Player" && other.gameObject.tag != "Ladder")
        {
            if(CompareTag("Dynamite"))
            {
                gameManager.instance.soundManager.aud.Stop();
                gameManager.instance.soundManager.aud.PlayOneShot(gameManager.instance.soundManager.dynamiteHit);
            }
            if(CompareTag("Bullet"))
            {
                gameManager.instance.soundManager.aud.PlayOneShot(gameManager.instance.soundManager.bulletHit);
            }
            anim.SetBool("explode", true);
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            Destroy(gameObject, anim.GetCurrentAnimatorStateInfo(0).length);
        }
    }
}