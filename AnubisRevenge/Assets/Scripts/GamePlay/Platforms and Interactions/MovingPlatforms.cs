using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    public bool vertical;
    public bool constant;
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
        if (constant)
        {
            if (vertical)
            {
                transform.position = new Vector3(transform.position.x,
                                                 Mathf.PingPong(Time.realtimeSinceStartup * speed, distance) + originalPosY,
                                                 transform.position.z);
            }
            else if (!vertical)
            {
                transform.position = new Vector3(Mathf.PingPong(Time.realtimeSinceStartup * speed, distance),
                                                 transform.position.y,
                                                 transform.position.z);
            }
        }
        else if (!constant)
        {
            if (vertical)
            {
                transform.position = new Vector3(transform.position.x, (transform.position.y + distance) * speed * Time.deltaTime, transform.position.z);
            }
            else if (!vertical)
            {
                transform.position = new Vector3((transform.position.x + distance) * speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
        }
    }

    
}
