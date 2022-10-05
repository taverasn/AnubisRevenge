using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    BMMovement inSight;
    private Transform feet;
    private Transform destination;
    
    private float DirectionX;
    private float DirectionY;
    
    public bool teleported;
    private float timedTP = 5;
    private float distance;

    void Start()
    {
        inSight = GetComponent<BMMovement>();
        distance = Vector2.Distance(GameObject.FindWithTag("Player").transform.position, transform.position);
        destination = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        DirectionX = destination.position.x;
        DirectionY = destination.position.y;
    }

    void Update()
    {
        timedTP -= Time.deltaTime;

        if(timedTP <= 0.0f)
        {
            EnemyTeleported();
        }
    }

    void EnemyTeleported()
    {
        if (inSight.behind == false)
        {
            if (DirectionX < 0.0f && DirectionY > 0.0f)
            {
                gameObject.transform.position = new Vector3(destination.position.x - 5, destination.position.y + 5, 0f);
            }
            else if (DirectionX > 0.0f && DirectionY < 0.0f)
            {
                gameObject.transform.position = new Vector3(destination.position.x + 5, destination.position.y + 5, 0f);
            }
        }
        else
        {
            if (DirectionX < 0.0f && DirectionY > 0.0f)
            {
                gameObject.transform.position = new Vector3(destination.position.x + 5, destination.position.y + 5, 0f);
            }
            else if (DirectionX > 0.0f && DirectionY < 0.0f)
            {
                gameObject.transform.position = new Vector3(destination.position.x - 5, destination.position.y + 5, 0f);
            }
        }
        
        timedTP = 5;
    }
}
