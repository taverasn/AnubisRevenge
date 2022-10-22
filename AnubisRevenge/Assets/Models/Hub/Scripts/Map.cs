using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    public GameObject mapHint;
    public GameObject map;

    public bool mapActive;
    public bool onMap;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && onMap)
        {
            ShowMap();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            mapHint.SetActive(true);
            onMap = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            mapHint.SetActive(false);
            onMap = false;
        }
    }

    public void ShowMap()
    {
        mapActive = !mapActive;

        if (mapActive)
        {
            CursorPause();
            mapHint.SetActive(!mapActive);
            map.SetActive(mapActive);
        }
        else
        {
            CursorUnpause();
            mapHint.SetActive(!mapActive);
            map.SetActive(mapActive);
        }
    }
    
    public void CursorPause()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void CursorUnpause()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
