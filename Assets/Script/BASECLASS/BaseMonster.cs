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
    NavMeshAgent navMesh;
    //temp stat
    float maxHealth;
    float damage;
    float expDrop;
    float currentHeath;
    public bool Attack;
    public int attackType = 1;
    bool death = false;
    public bool Death => death;
    float coolDown;
    public float CoolDown { get { return coolDown; } set { coolDown = value; } }
    public LayerMask arrow;

    //Minion
    [Header("Minion")]
    public bool axeEnable=false;
    public Action DeathTrigger;
    public Action HeadHit;
    public Action BodyHit;
    public Transform center;
    public Vector3 size;

    //Collider to disable
    public Collider head;
    public Collider body;
    //----vfx----
    GameObject vfx;
    //------------test-------------
    float burnTime = 1f;
    bool burning = false;
    Coroutine IceSpeed;


    void Awake()
    {
        GameManager.ins.monsterStatIncrease += IncreaseStat;
        navMesh=GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        currentHeath = stat.health;
        maxHealth = stat.health;
        damage = stat.damage;
        coolDown = stat.baseCoolDown;
        expDrop = stat.expDrop;
        GameManager.ins.CountDownComplete += Despawn;
        
    }
    public void EnableValue() {         
        currentHeath = maxHealth;
        death = false;
        Attack = true;
        coolDown = stat.baseCoolDown;
        head.enabled = true;
        body.enabled = true;
        navMesh.isStopped = false;
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
            GameManager.ins.LevelChange?.Invoke(stat.expDrop,stat.coins);
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
    void DamageOverTime(float damage) {
        currentHeath -= damage;
        currentHeath = Mathf.Clamp(currentHeath, 0, maxHealth);
        UIManager.ins.ChangeIconDamage();
        if (currentHeath <= 0)
        {
            Attack = false;
            death = true;
            stat.velocity = 0;
            //send coins
            GameManager.ins.LevelChange?.Invoke(stat.expDrop, stat.coins);
            //trigger Death animation
            DeathTrigger.Invoke();
            //disable collider
            head.enabled = false;
            body.enabled = false;
            GameManager.ins.monsterOnFieldCount--;
        }
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
    void IncreaseStat() {
        maxHealth += 1.2f;
        damage += 1.2f;
        currentHeath = maxHealth;
        //stat.expDrop = Mathf.RoundToInt(stat.expDrop * 1.2f);
    }
    public void IncreaseStatWithLevel(int currentLevel)
    {
        maxHealth += 1.2f * (currentLevel - 1);
        damage += 1.2f*( currentLevel-1);       
        currentHeath = maxHealth;
        //expDrop = Mathf.RoundToInt(stat.expDrop * Mathf.Pow(1.2f, currentLevel-1));
    }

    //----------------------Elemental damage--------------------------

    public void DamgeType(bool fire, bool ice,float damage)
    {
        if (fire)
        {
            //spawn fire vfx
            vfx = EffectSpawn.ins.GetEffect(EffectName.Fire);
            vfx.transform.position = transform.position;
            vfx.transform.parent = transform;
            //take damage overtime
            if (!burning)
            {
                burning = true;
                StartCoroutine(FireDamgeOverTime(damage, burnTime)); 
            }
        }
        else if (ice)
        {
            if (IceSpeed != null)
                StopCoroutine(IceSpeed);    
            IceSpeed = StartCoroutine(IceReturnSpeed());
            navMesh.speed -= navMesh.speed * damage / 100;
            navMesh.speed=Mathf.Max(1,navMesh.speed);

        }
    }
    IEnumerator FireDamgeOverTime(float damage,float burnTime)
    {
        if (burnTime <= 0)
        {
            burning = false;
            Debug.Log("Burnt done");
            EffectSpawn.ins.ReturnEffect(vfx, EffectName.Fire);
            yield return null; 
        }
        DamageOverTime(damage);
        burnTime -= Time.deltaTime;
        yield return new WaitForSeconds(0.1f);
    }
    //ice damage problem
    IEnumerator IceReturnSpeed()
    {

        yield return new WaitForSeconds(1f);
        //wait for 1 sec if take no ice damage then return speed to normal
        navMesh.speed = stat.speed;
    }
}
