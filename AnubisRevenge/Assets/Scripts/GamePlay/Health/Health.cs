using UnityEngine;
using System.Collections;


public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;
    public float despawnTimer;
    [SerializeField] private Behaviour[] components;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;
    private PlayerController playerController;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
        if(gameObject.tag == "Player")
        {
            playerController = gameObject.GetComponent<PlayerController>();
        }
    }

    private void Update()
    {
        if (dead == true && gameObject.tag == "Enemy")
        {
            despawnTimer += Time.deltaTime;
            startFading();
        }
        if(dead == true && gameObject.tag == "Enemy" && despawnTimer >= 5)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        Debug.Log("Took damage");
        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invunerability());
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("hurt");
                anim.SetBool("Dead", true);
                foreach (Behaviour component in components)
                {
                    if(component.name != "Health" && component.name != "PlayerController")
                        component.enabled = false;
                }
                if(gameObject.tag == "Player")
                {
                    playerController.gameOver = true;
                }
                dead = true;
            }
        }
    }
    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    private IEnumerator FadeOut()
    {
        for (float f = 1f; f >= -0.05f; f -= 0.05f)
        {
            Color c = spriteRend.material.color;
            c.a = f;
            spriteRend.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }
    }

    private void startFading()
    {
        StartCoroutine("FadeOut");
    }
    private IEnumerator Invunerability()
    {
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
    }
}