using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings: MonoBehaviour
{
    public static GameSettings ins;
    SoundBrightSen settings;
    //Game Pause
    public bool isGamePaused = false;

    public float mouseSenMultiply = 1000;
    public float mouseSensivity = 1f;
    public float brightness = 1f;
    public float soundVolume = 1f;

    //Pause Menu
    public GameObject pauseMenu;

    public bool gameComplete = false;

    public bool othersPanel = false;

    private void Awake()
    {
        if (ins != null && ins != this)
            Destroy(this);
        else
            ins = this;
        
    }
    private void Start()
    {
        settings=FindObjectOfType<SoundBrightSen>();
        if (!settings)
        {
            
        }
        else
        {
            Debug.Log("mouse sense from menu: "+settings.sen);
            mouseSensivity = settings.sen * mouseSenMultiply;
            brightness = settings.brightSaved;
            soundVolume = settings.sound;
            
        }
           
        
    }
    void Update()
    {
        SetGamePause();
    }
    void SetGamePause()
    {
        if (Input.GetKeyDown(KeyCode.Escape)&&!gameComplete&&!othersPanel)
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
