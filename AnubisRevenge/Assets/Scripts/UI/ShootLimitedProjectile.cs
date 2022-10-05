using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShootLimitedProjectile : MonoBehaviour
{
    [SerializeField] public Image[] dynImages;
    [SerializeField] private int numOfDynamite;

    // Set Default Dynamite Amount
    

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("dynamite", numOfDynamite);

        // To start with no ammo to start
        foreach(var image in dynImages)
        {
            image.enabled = true;
        }
    }

    public void UseDynamite()
    {
        dynImages[(PlayerPrefs.GetInt("dynamite")) - 1].enabled = false;
        PlayerPrefs.SetInt("dynamite", PlayerPrefs.GetInt("dynamite")-1);
    }
    
}
