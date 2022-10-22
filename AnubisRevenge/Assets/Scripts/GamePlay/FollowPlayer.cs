using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public float offset;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(gameManager.instance.player.transform.position.x, gameManager.instance.player.transform.position.y + offset, transform.position.z);
    }
}
