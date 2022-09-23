using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    public Transform detectionPoint;
    private float detectionRadius = .75f;
    public LayerMask detectionLayer;

    public GameObject interactionObject;
    private Levers lever;

    // Update is called once per frame
    void Update()
    {
        //detect if object is in area
        if (DetectObject())
        {
            //set the object to interactionObject
            SetInteractionObject();

            //if player presses E
            if (InteractInput())
            {
                Debug.Log("Interacted");

                //if object is a lever
                if (interactionObject.CompareTag("Levers"))
                {
                    lever = interactionObject.GetComponent<Levers>();
                    lever.ActivateMovingPlatforms();
                }
            }
        }
    }

    //Press E to interact
    bool InteractInput()
    {
        return Input.GetKeyDown(KeyCode.E);
    }

    //Detect an object that player can interact with
    bool DetectObject()
    {
        return Physics2D.OverlapCircle(detectionPoint.position, detectionRadius, detectionLayer);
    }
    
    //assign the interactionObject to the object near the player
    void SetInteractionObject()
    {
        interactionObject = Physics2D.OverlapCircle(detectionPoint.position, detectionRadius, detectionLayer).gameObject;
    }
}
