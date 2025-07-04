using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SO", menuName = "ScriptableObjects/BaseCharacterStats")]

public class SO_Char : ScriptableObject
{
    public float health;
    public float damage;
    public float speed;
}
