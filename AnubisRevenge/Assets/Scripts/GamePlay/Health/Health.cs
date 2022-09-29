using UnityEngine;
using System.Collections;


public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;
    private bool isDamaged;
    public float despawnTimer;
    [SerializeField] private Behaviour[] components;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;
    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
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
        if(gameObject.tag == "Player")
        {
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

    public bool GetIsDamaged()
    {
        return isDamaged;
    }

    public void SetIsDamaged(bool _isDamaged)
    {
        isDamaged = _isDamaged;
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