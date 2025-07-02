using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SO", menuName = "ScriptableObjects/BaseCharacterStats")]

public class SO_Char : ScriptableObject
{
    float health;
    float damage;
    float speed;
}
