using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public Animator animator;
    private string currentState;
    const string BM_IDLE = "BM_Idle";
    const string BM_HURT = "BM_Hurt";

    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth;
    [SerializeField] private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    public float despawnTimer;
    private SpriteRenderer spriteRend;
    void Start()
    {
        
    }

    
    void Update()
    {
        if (dead == true && gameObject.tag == "Enemy")
        {
            despawnTimer += Time.deltaTime;
            startFading();
        }
        if (dead == true && gameObject.tag == "Enemy" && despawnTimer >= 5)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, maxHealth);
        if (currentHealth > 0)
        {
            if (gameObject.tag == "Enemy")
            {
                ChangeAnimationState(BM_HURT);
            }
            StartCoroutine(Invunerability());
        }
    }

     void ChangeAnimationState(string newState)
    {
        // stop same animation from interrupting itself
        if (currentState == newState) return;

        // play the animation
        animator.Play(newState);

        // reassign the current state
        currentState = newState;
    }

    public void StartingHealth(int hp)
    {
        currentHealth = hp;
        maxHealth = hp;
        dead = false;
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
