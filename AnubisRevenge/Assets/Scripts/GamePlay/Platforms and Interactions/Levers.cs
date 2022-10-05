using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levers : MonoBehaviour
{
    public GameObject activatedObject;

    public void ActivateMovingPlatforms()
    {
        activatedObject.GetComponent<MovingPlatforms>().switchDirectionsN = !activatedObject.GetComponent<MovingPlatforms>().switchDirectionsN;
    }
}
