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
    void Awake()
    {
        GameManager.ins.monsterStatIncrease += IncreaseStat;
    }
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
            {
                GameObject effect= EffectSpawn.ins.GetEffect(EffectName.Normal);
                effect.transform.position = hitPoint;
                BodyHit.Invoke();
            }
            else
            {
                GameObject effect = EffectSpawn.ins.GetEffect(EffectName.Head);
                effect.transform.position = hitPoint;
                HeadHit.Invoke(); 
            }
                    
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

    public void FireDamage(float damage, Vector3 hitPoint, Vector3 hitDirection, GameObject attacker, IDamageable.Body type)
    {
        throw new NotImplementedException();
    }

    public void IceDamage(float damage, Vector3 hitPoint, Vector3 hitDirection, GameObject attacker, IDamageable.Body type)
    {
        throw new NotImplementedException();
    }
    void IncreaseStat() {
        stat.health *= 1.2f;
        stat.damage *= 1.2f;
        maxHealth = stat.health;
        currentHeath = maxHealth;
        //stat.expDrop = Mathf.RoundToInt(stat.expDrop * 1.2f);
    }
    public void IncreaseStatWithLevel(int currentLevel)
    {
        stat.health *= Mathf.Pow(1.2f,currentLevel-1);
        stat.damage *= Mathf.Pow(1.2f, currentLevel-1);
        maxHealth = stat.health;
        currentHeath = maxHealth;
        //stat.expDrop = Mathf.RoundToInt(stat.expDrop * 1.2f);
    }

}
