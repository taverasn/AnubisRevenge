using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levers : MonoBehaviour
{
    public GameObject activatedObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateMovingPlatforms()
    {
        activatedObject.GetComponent<MovingPlatforms>().switchDirections = !activatedObject.GetComponent<MovingPlatforms>().switchDirections;
    }
}
