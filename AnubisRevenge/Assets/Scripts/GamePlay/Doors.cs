using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Doors : MonoBehaviour
{
    public string sceneToLoad;

    public void EnterDoor()
    {
        SceneManager.LoadScene(sceneToLoad);
    }


}
