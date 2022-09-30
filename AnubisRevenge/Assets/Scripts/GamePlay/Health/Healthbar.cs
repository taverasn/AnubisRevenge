using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;

    private void Start()
    {
        //SetMaxHealth(playerHealth.currentHealth);
    }
    private void Update()
    {
/*        if (playerHealth.isDamaged)
            SetHealth(playerHealth.currentHealth);*/
    }
}
