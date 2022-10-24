using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonFunctions : MonoBehaviour
{
    public void resume()
    {
        gameManager.instance.cursorUnLockUnPause();
        gameManager.instance.isPaused = !gameManager.instance.isPaused;
        gameManager.instance.pauseMenu.SetActive(gameManager.instance.isPaused);
    }
    public void restart()
    {
        gameManager.instance.cursorUnLockUnPause();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void quit()
    {
        SceneManager.LoadScene("Menus");
    }
    public void respawn()
    {
        gameManager.instance.pCtrl.respawn();
        gameManager.instance.cursorUnLockUnPause();
    }

    public void returnToHub()
    {
        SceneManager.LoadScene("Hub");
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menus");
    }

    public void LoadSettings()
    {
        Debug.Log("Settings are Opening...");
    }
}

