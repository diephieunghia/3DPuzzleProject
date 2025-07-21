using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;
public class BaseMonster : MonoBehaviour,IDamageable
{
    [SerializeField] SO_Mons stat;
    public SO_Mons MonsterStat => stat;


    float currentHeath;
    float maxHealth;
    public bool Attack;
    bool death = false;
    public bool Death => death;


    public Action DeathTrigger;
    public Action HeadHit;
    public Action BodyHit;
    private void Start()
    {
        currentHeath = stat.health;
        maxHealth = stat.health;
    }

    public void TakeDamage(float damage, Vector3 hitPoint, Vector3 hitDirection, GameObject attacker,IDamageable.Body hitPart)
    {
        currentHeath -= damage;
        currentHeath = Mathf.Clamp(currentHeath, 0, maxHealth);
        if(currentHeath <= 0)
        {
            Attack = false;
            death = true;
            stat.velocity = 0;
            DeathTrigger.Invoke();
        }
        else
        {
            if (hitPart == IDamageable.Body.Body)
                BodyHit.Invoke();
            else
                HeadHit.Invoke();
                    
        }
    }
}
