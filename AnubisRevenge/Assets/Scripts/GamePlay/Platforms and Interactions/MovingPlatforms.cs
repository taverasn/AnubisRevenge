using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    //if ending in C it is only for Constant moving platforms
    //if ending in N it is only for Non-constant moving platforms

    public bool vertical;
    public bool constant;
    public bool reverseC;
    public bool switchDirectionsN = false;

    public float speed;
    public float distance;

    private float originalPosX;
    private float originalPosY;


    //reverse does not work for non-constant platforms
    //non-constant platforms start in the middle
    //speed for non-constant is how many frames it moves each movement (so use small number like .015)
    //switchDirections bool is only for non-constant

    // Start is called before the first frame update
    void Start()
    {
        originalPosX = transform.position.x;
        originalPosY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    private void FixedUpdate()
    {
        //if you want the platform to constantly be moving
        if (constant)
        {
            //if you want the platform to move in a negative direction
            if (reverseC)
            {
                //if you want the platform to move vertically
                if (vertical)
                {
                    //move back and forth
                    transform.position = new Vector3(transform.position.x,
                                                     -Mathf.PingPong(Time.realtimeSinceStartup * speed, distance) + originalPosY,
                                                     transform.position.z);
                }
                // horizontally
                else if (!vertical)
                {
                    //move back and forth
                    transform.position = new Vector3(-Mathf.PingPong(Time.realtimeSinceStartup * speed, distance) + originalPosX,
                                                     transform.position.y,
                                                     transform.position.z);
                }
            }
            //platforming moving in positive direction
            else
            {
                //vertical
                if (vertical)
                {
                    //move back and forth
                    transform.position = new Vector3(transform.position.x,
                                                     Mathf.PingPong(Time.realtimeSinceStartup * speed, distance) + originalPosY,
                                                     transform.position.z);
                }
                //horizontal
                else if (!vertical)
                {
                    //move back and forth
                    transform.position = new Vector3(Mathf.PingPong(Time.realtimeSinceStartup * speed, distance) + originalPosX,
                                                     transform.position.y,
                                                     transform.position.z);
                }
            }
        }
        if (!constant)
        {
            //no reverse needed
            //vertical
            if (vertical)
            {
                //go one direction
                if (switchDirectionsN)
                {
                    //move
                    transform.position = new Vector3(transform.position.x,
                                                     Mathf.MoveTowards(transform.position.y, originalPosY + distance, speed/100),
                                                     transform.position.z);
                }
                //go other direction
                else if (!switchDirectionsN)
                {
                    transform.position = new Vector3(transform.position.x,
                                                     Mathf.MoveTowards(transform.position.y, originalPosY - distance, speed / 100),
                                                     transform.position.z);
                }
            }
            //horizontal
            else if (!vertical)
            {
                //go one direction
                if (switchDirectionsN)
                {
                    //move
                    transform.position = new Vector3(Mathf.MoveTowards(transform.position.x, originalPosX - distance, speed / 100),
                                                     transform.position.y,
                                                     transform.position.z);
                }
                //go other direction
                else if (!switchDirectionsN)
                {
                    transform.position = new Vector3(Mathf.MoveTowards(transform.position.x, originalPosX + distance, speed / 100),
                                                     transform.position.y,
                                                     transform.position.z);
                }
            }
        }
    }

}
