using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    public bool vertical;
    public bool constant;
    public bool reverse;

    public float speed;
    public float distance;

    private float originalPosX;
    private float originalPosY;

    // Start is called before the first frame update
    void Start()
    {
        originalPosX = transform.position.x;
        originalPosY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        //if you want the platform to constantly be moving
        if (constant)
        {
            //if you want the platform to move in a negative direction
            if (reverse)
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
        //not constantly moving
        else if (!constant)
        {
            //no reverse needed
            //vertical
            if (vertical)
            {
                //move
                transform.position = new Vector3(transform.position.x, (transform.position.y + distance) * speed * Time.deltaTime, transform.position.z);
            }
            //horizontal
            else if (!vertical)
            {
                //move
                transform.position = new Vector3((transform.position.x + distance) * speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
        }
    }

    
}
