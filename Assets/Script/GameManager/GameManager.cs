using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager ins { get; private set; }

    //level
    public Action<float> LevelChange;

    private void Awake()
    {
        if (ins != null && ins != this)
            Destroy(this);
        else
            ins = this;
    }
}
