using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject settings;

    // Start is called before the first frame update
    public void StartButton(GameObject loadingScreen)
    {
        loadingScreen.SetActive(true);
        SceneManager.LoadScene("MainLevel");
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Restart()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene("MainLevel");
        Time.timeScale = 1f;
    }
    public void Resume()
    {
        GameSettings.ins.isGamePaused=!GameSettings.ins.isGamePaused;
        Time.timeScale = GameSettings.ins.isGamePaused ? 0f : 1f;
        GameSettings.ins.SetActive();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }
    public void BacktoMenu(GameObject settingPanel)
    {
        settingPanel.SetActive(false);
    }
    public void Settings(GameObject settingPanel)
    {
        settingPanel.SetActive(true);
    }
}
