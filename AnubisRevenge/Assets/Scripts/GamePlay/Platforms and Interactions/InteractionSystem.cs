using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    public Transform detectionPoint;
    public float detectionRadius;
    public LayerMask detectionLayer;

    public GameObject interactionObject;

    // Update is called once per frame
    void Update()
    {
        if (DetectObject())
        {
            SetInteractionObject();

            if (InteractInput())
            {
                Debug.Log("Interacted");

                if (interactionObject.CompareTag("Levers"))
                {

                }
            }
        }
    }

    bool InteractInput()
    {
        return Input.GetKeyDown(KeyCode.E);
    }

    bool DetectObject()
    {
        return Physics2D.OverlapCircle(detectionPoint.position, detectionRadius,detectionLayer);
    }
    private void OnCollisionEnter(Collision collision)
    {
        interactionObject = collision.gameObject;
    }
    
    void SetInteractionObject()
    {
        interactionObject = Physics2D.OverlapCircle(detectionPoint.position, detectionRadius, detectionLayer).gameObject;
    }
}
