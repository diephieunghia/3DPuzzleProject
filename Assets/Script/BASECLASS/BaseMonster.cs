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
    public int attackType = 1;
    bool death = false;
    public bool Death => death;
    float coolDown;
    public float CoolDown { get { return coolDown; } set { coolDown = value; } }
    public LayerMask arrow;

    //Minion
    public bool axeEnable=false;
    public Action DeathTrigger;
    public Action HeadHit;
    public Action BodyHit;
    public Transform center;
    public Vector3 size;

    //coins
    public float coins = 10f;

    //Collider to disable
    public Collider head;
    public Collider body;
    private void Start()
    {
        currentHeath = stat.health;
        maxHealth = stat.health;
        coolDown = stat.baseCoolDown;
        GameManager.ins.CountDownComplete += Despawn;
    }

    public void TakeDamage(float damage, Vector3 hitPoint, Vector3 hitDirection, GameObject attacker,IDamageable.Body hitPart)
    {
        currentHeath -= damage;
        currentHeath = Mathf.Clamp(currentHeath, 0, maxHealth);
        UIManager.ins.ChangeIconDamage();
        if (currentHeath <= 0)
        {
            Attack = false;
            death = true;
            stat.velocity = 0;
            //send coins
            GameManager.ins.LevelChange?.Invoke(stat.expDrop,coins);
            //trigger Death animation
            DeathTrigger.Invoke();
            //disable collider
            head.enabled = false;
            body.enabled = false;
            GameManager.ins.monsterOnFieldCount--;
        }
        else
        {
            
            if (hitPart == IDamageable.Body.Body)
                BodyHit.Invoke();
            else
                HeadHit.Invoke();
                    
        }
    }
    private void OnDrawGizmos()
    {
        
        Gizmos.DrawWireCube(center.position, size);
    }
    void Despawn()
    {
        ObjectPool monsterPool=gameObject.GetComponentInParent<MonsterPool>();
        if (monsterPool.poolObjects.Count>=20)
        {
            GameManager.ins.CountDownComplete -= Despawn;
        }
        head.enabled = true;
        body.enabled = true;

        monsterPool.ReturnObject(gameObject);
    }
}
