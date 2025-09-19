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

    void Start()
    {
        GameCompleteAction+= SetGamePause;
    }
    public void SetGamePause(bool complete)
    {
        GameSettings.ins.isGamePaused = !GameSettings.ins.isGamePaused;
        Time.timeScale = GameSettings.ins.isGamePaused ? 0f : 1f;
        GameSettings.ins.gameComplete = true;
        Debug.Log("Game Complete");
        if (complete)
        { 
            Debug.Log("You Win"); 
            if(!WinUI.activeSelf)
                WinUI.SetActive(true);
        }
        else
        {
            Debug.Log("You Lose");
            if (!LoseUI.activeSelf)
                LoseUI.SetActive(true);
        }
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
