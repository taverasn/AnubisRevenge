using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public static gameManager instance;

    public soundManager soundManager;
    [Header("----- Player Components -----")]
    public GameObject player;
    public PlayerController pCtrl;

    [Header("----- UI Components -----")]
    public GameObject pauseMenu;
    public GameObject playerDeadMenu;
    public GameObject winMenu;
    public GameObject currentMenu;
    public GameObject spawnPosition;
    public HealthBar healthBar;
    public ShootLimitedProjectile limitedProjectile;

    public bool isPaused;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        pCtrl = player.GetComponent<PlayerController>();
        spawnPosition = GameObject.FindGameObjectWithTag("Spawn Position");
        soundManager.aud.PlayOneShot(soundManager.levelMusic, soundManager.levelMusicVol);

    }
    private void Start()
    {
        enablePlayer(true);
        cursorUnLockUnPause();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel") && !playerDeadMenu.activeSelf && !winMenu.activeSelf)
        {
            isPaused = !isPaused;
            pauseMenu.SetActive(isPaused);
            if (pauseMenu.activeSelf)
            {
                cursorLockPause();
            }
            else
            {
                cursorUnLockUnPause();
            }
        }
        if (pauseMenu.activeSelf)
        {
            enablePlayer(false);
        }
        else
        {
            enablePlayer(true);
        }
    }

    public void enablePlayer(bool enable)
    {
        pCtrl.pMove.enabled = enable;
        pCtrl.pAttack.enabled = enable;
        pCtrl.pInput.enabled = enable;
        pCtrl.pAnimHandler.enabled = enable;
        pCtrl.pColl.enabled = enable;
        pCtrl.enabled = enable;

    }

    public void cursorLockPause()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void cursorUnLockUnPause()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
