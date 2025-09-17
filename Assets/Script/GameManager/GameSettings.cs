using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings: MonoBehaviour
{
    public static GameSettings ins;
    //Game Pause
    public bool isGamePaused = false;


    public float mouseSensivity = 100f;
    public float brightness = 1f;
    public float soundVolume = 1f;

    //Pause Menu
    public GameObject pauseMenu;

    public bool gameComplete = false;

    private void Awake()
    {
        if (ins != null && ins != this)
            Destroy(this);
        else
            ins = this;
    }
    void Update()
    {
        SetGamePause();
    }
    void SetGamePause()
    {
        if (Input.GetKeyDown(KeyCode.Escape)&&!gameComplete)
        {
            isGamePaused = !isGamePaused;
            Time.timeScale = isGamePaused ? 0f : 1f;
            SetActive();
            Cursor.visible = isGamePaused;
            Cursor.lockState = CursorLockMode.None;
        }
    }
    public void SetActive()
    {
        pauseMenu.SetActive(isGamePaused);
    }

    

}
