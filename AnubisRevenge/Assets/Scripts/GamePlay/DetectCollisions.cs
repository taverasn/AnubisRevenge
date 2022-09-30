using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectCollisions : MonoBehaviour
{
    private Health playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.Find("PlayerCharacter").GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
    }



}
