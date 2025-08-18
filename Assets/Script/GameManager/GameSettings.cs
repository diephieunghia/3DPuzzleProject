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

    private void Awake()
    {
        if (ins != null && ins != this)
            Destroy(this);
        else
            ins = this;
    }

}
