using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SO", menuName = "ScriptableObjects/BaseMonsterStats", order = 2)]

public class SO_Mons : ScriptableObject
{
    public float health;
    public float damage;
    public float speed;
    public bool attack;
    public bool shield;
}
