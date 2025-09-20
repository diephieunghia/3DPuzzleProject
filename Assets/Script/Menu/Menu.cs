using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject settings;
    private void Start()
    {
        settings = GameObject.FindGameObjectWithTag("Settings");
    }
    // Start is called before the first frame update
    public void StartButton(GameObject loadingScreen)
    {
        loadingScreen.SetActive(true);
        SceneManager.LoadScene("MainLevel");
        if(settings)
            DontDestroyOnLoad(settings);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Restart()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene("MainLevel");
        if (settings)
            DontDestroyOnLoad(settings);
        Time.timeScale = 1f;
    }
    public void Resume()
    {
        GameSettings.ins.isGamePaused=!GameSettings.ins.isGamePaused;
        Time.timeScale = GameSettings.ins.isGamePaused ? 0f : 1f;
        GameSettings.ins.SetActive();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;
    }
    public void BacktoMenu(GameObject settingPanel)
    {
        settingPanel.SetActive(false);
        if (GameSettings.ins)
            GameSettings.ins.othersPanel = false;
    }
    public void Settings(GameObject settingPanel)
    {
        settingPanel.SetActive(true);
        if (GameSettings.ins)
            GameSettings.ins.othersPanel = true;
    }
}
