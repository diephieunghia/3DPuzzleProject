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

    //Pause Menu
    public GameObject pauseMenu;

    //test sky box
    public Material skybox;
    public Material skyBox2;
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
        TestSkyBox();
    }
    void SetGamePause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isGamePaused = !isGamePaused;
            Time.timeScale = isGamePaused ? 0f : 1f;
            pauseMenu.SetActive(isGamePaused);
        }
    }
    void TestSkyBox()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            RenderSettings.skybox = skybox;
        }
    }

}
