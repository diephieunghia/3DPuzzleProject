using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCompleteOrDie : MonoBehaviour
{
    public bool GameComplete = false;
    public Action<bool> GameCompleteAction;
    public GameObject LoseUI;
    public GameObject WinUI;

    public AudioSource BG_Music;

    void Start()
    {
        GameCompleteAction+= SetGamePause;
    }
    public void SetGamePause(bool complete)
    {
        GameSettings.ins.isGamePaused = !GameSettings.ins.isGamePaused;
        Time.timeScale = GameSettings.ins.isGamePaused ? 0f : 1f;
        GameSettings.ins.gameComplete = true;
        if (complete)
        { 
            Debug.Log("You Win"); 
            if(!WinUI.activeSelf)
                WinUI.SetActive(true);
            BG_Music.Stop();
        }
        else
        {
            Debug.Log("You Lose");
            if (!LoseUI.activeSelf)
                LoseUI.SetActive(true);
            BG_Music.Stop();
        }
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
