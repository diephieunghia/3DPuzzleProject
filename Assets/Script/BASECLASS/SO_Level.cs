using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SO", menuName = "ScriptableObjects/LevelStat")]
public class SO_Level : ScriptableObject
{
    public int currentLevel = 1;
    public int maxLevel = 10;

    public float monsterPowerScale = 1.2f;
}
