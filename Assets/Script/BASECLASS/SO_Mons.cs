using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SO", menuName = "ScriptableObjects/BaseMonsterStats", order = 2)]

public class SO_Mons : ScriptableObject
{
    public float health;
    public float damage;
    public float speed;

    //anim stat
    public bool attack=false;
    public bool shield=false;
    public bool hitBody = false;
    public bool hitHead = false;
    public float baseCoolDown;
    public bool death=false;
    public float velocity;

    public float attackRate;

    
}
