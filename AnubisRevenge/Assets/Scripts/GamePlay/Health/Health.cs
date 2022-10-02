using UnityEngine;
using System.Collections;


public class Health : MonoBehaviour
{
    private PlayerController pCtrl;
    [Header("Health")]
    [SerializeField] private float startingHealth;
    internal float currentHealth { get; private set; }
    [SerializeField] private HealthBar healthBar;
    private Animator anim;
    private bool dead;
    internal bool isDamaged;
    [SerializeField ]private float despawnDelay;
    [SerializeField] private Behaviour[] components;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;
    private void Start()
    {
        pCtrl = GetComponent<PlayerController>();
        currentHealth = startingHealth;

        if (gameObject.tag == "Player")
        {
            healthBar.SetMaxHealth(currentHealth);
        }

        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (dead == true && gameObject.tag == "Enemy")
        {
            startFading();
            Destroy(gameObject.transform.parent.gameObject, 5);
        }
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);


        if(gameObject.tag == "Player")
        {
            Debug.Log(PlayerPrefs.GetInt("dynamite"));
            healthBar.SetHealth(currentHealth);
            isDamaged = true;
        }
        else
        {
            if (currentHealth > 0)
            {
                anim.SetTrigger("hurt");
                StartCoroutine(Invunerability());
            }
        }
        if (!dead && currentHealth <= 0 && gameObject.tag != "Player")
        {
            if(currentHealth <= 0)
            {
                    anim.SetTrigger("hurt");
                    anim.SetBool("Dead", true);
            }
            dead = true;
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