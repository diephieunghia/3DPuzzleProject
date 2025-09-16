using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCompleteOrDie : MonoBehaviour
{
    public bool GameComplete = false;
    public Action GameCompleteAction;


    void Start()
    {
        GameCompleteAction+= SetGamePause;
    }
    public void SetGamePause()
    {
        GameSettings.ins.isGamePaused = !GameSettings.ins.isGamePaused;
        Time.timeScale = GameSettings.ins.isGamePaused ? 0f : 1f;
        GameSettings.ins.gameComplete = true;
        Debug.Log("Game Complete");
    }
}
