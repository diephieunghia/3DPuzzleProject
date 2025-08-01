using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStatSkill : MonoBehaviour
{
    //Part of game manager, this is used to accumulate level and skill points
    //This is a singleton class, only one instance should exist in the game
    public static LevelStatSkill ins { get; private set; }


    int levelText = 1;
    float currentEXP = 0f;
    float maxEXP;
    public float MaxEXP { set { maxEXP = value; }}
    public Action<float> LevelAccumulate;
    private void Awake()
    {
        if (ins != null && ins != this)
            Destroy(this);
        else
            ins = this;

    }
    private void Start()
    {
        LevelAccumulate += CalculateLevel;
    }
    private void CalculateLevel(float value)
    {
            
    }




}
