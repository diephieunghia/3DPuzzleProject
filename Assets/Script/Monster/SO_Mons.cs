using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SO", menuName = "ScriptableObjects/BaseMonsterStats", order = 2)]

public class SO_Mons : ScriptableObject
{
    float health;
    float damage;
    float speed;
}
