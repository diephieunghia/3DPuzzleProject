using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BaseMonster : MonoBehaviour,IDamageable
{
    [SerializeField] SO_Mons stat;
    public SO_Mons MonsterStat => stat;


    float currentHeath;
    float maxHealth;

    public Action DeathTrigger;
    private void Start()
    {
        currentHeath = stat.health;
        maxHealth = stat.health;
    }

    public void TakeDamage(float damage, Vector3 hitPoint, Vector3 hitDirection, GameObject attacker)
    {
        currentHeath -= damage;
        currentHeath = Mathf.Clamp(currentHeath, 0, maxHealth);
        if(currentHeath <= 0)
        {
            stat.death = true;
            stat.attack = false;
            stat.velocity = 0;
            DeathTrigger.Invoke();
        }
        else
        {           
        }
    }
}
