using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public static gameManager instance;

    public GameObject player;
    public PlayerController pCtrl;

    public HealthBar healthBar;
    public ShootLimitedProjectile limitedProjectile;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        pCtrl = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}