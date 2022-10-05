using UnityEngine;
using System.Collections;


public class EnemyHealth : MonoBehaviour, IDamage
{
    [Header("Health")]
    [SerializeField] private float startingHealth;
    internal float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;
    [SerializeField] private float despawnDelay;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;
    private void Start()
    {
        currentHealth = startingHealth;

        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (dead == true)
        {
            startFading();
            Destroy(gameObject, 2.5f);
        }
    }

    public void takeDamage(int dmg)
    {
        currentHealth -= dmg;
        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invunerability());
        }
        if (!dead && currentHealth <= 0)
        {
            anim.SetTrigger("hurt");
            anim.SetBool("Dead", true);
            dead = true;
        }
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