using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionSystem : MonoBehaviour
{
    //***************************Detection variables**********************//
    [SerializeField] Transform detectionPoint;
    private float detectionRadius = .75f;
    [SerializeField] LayerMask detectionLayer;


    //***************************Unity Event System**************************//
    [SerializeField] KeyCode interactKey;
    [SerializeField] UnityEvent interactAction;

    ///**************************Interaction Objects**************************//
    [SerializeField] GameObject interactionObject;
    private Levers lever;
    private Doors door;

    // Update is called once per frame
    void Update()
    {
        //detect if object is in area
        if (DetectObject())
        {
            //set the object to interactionObject
            SetInteractionObject();

            if (Input.GetKeyDown(interactKey))
            {
                interactAction.Invoke();
            }


            //*****************Without Unity Event System*********************//
            ////if player presses E
            //if (InteractInput())
            //{
            //    Debug.Log("Interacted");
            //
            //    InteractWithObject();
            //}
        }
    }

    //*********************Without Unity Event System***************************//
    //Press E to interact
    //bool InteractInput()
    //{
    //    return Input.GetKeyDown(KeyCode.E);
    //}

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

    //interact with an object based on the object's tag
    public void InteractWithObject()
    {
        //if object is a lever
        if (interactionObject.CompareTag("Levers"))
        {
            lever = interactionObject.GetComponent<Levers>();
            lever.ActivateMovingPlatforms();
        }
        else if (interactionObject.CompareTag("Doors"))
        {
            door = interactionObject.GetComponent<Doors>();
            door.EnterDoor();
        }
    }
}
