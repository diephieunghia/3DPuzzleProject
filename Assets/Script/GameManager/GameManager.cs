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
    public void StatLevelUp(ArcherBlackBoard stat)
    {

    }

    //timeCounter += Time.deltaTime;
    //    minutes = Mathf.FloorToInt(timeCounter / 60f);
    //    seconds = Mathf.FloorToInt(timeCounter - minutes* 60);
    //    timeCount.text = string.Format("{0:00}:{1:00}", minutes, seconds);
}
