using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShootLimitedProjectile : MonoBehaviour
{
    [SerializeField] private Image[] dynImages;
    [SerializeField] PlayerInput player;

    // Set Default Dynamite Amount
    

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("dynamite", 5);

        // To start with no ammo to start
        foreach(var image in dynImages)
        {
            image.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UseDynamite();
    }

    private void UseDynamite()
    {
        if (player.isThrowPressed)
        {
            // Disable image when fired
            dynImages[(PlayerPrefs.GetInt("dynamite")) - 1].enabled = false;

            PlayerPrefs.SetInt("dynamite", PlayerPrefs.GetInt("dynamite")-1);
        }


    }
    
}
