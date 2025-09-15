using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
[RequireComponent(typeof(MageAnim),typeof(BaseMonster))]
public class MageAttack : MonoBehaviour
{
    MageBehavior m_Behavior;
    MageAnim anim;
    BaseMonster m_Monster;
    float countDown = 5f;
    [SerializeField] GameObject fireBallHolder;

    void Start()
    {
        m_Monster = GetComponent<BaseMonster>();
        m_Behavior = GetComponent<MageBehavior>();
        anim = GetComponent<MageAnim>();
    }
    // Update is called once per frame
    void Update()
    {
        countDown -= Time.deltaTime;
        if (countDown <= 0 && m_Monster.Attack)
        {

            int rand = RandomWithProbability();
            if (rand == 1)
                AttackType1();
            else
                AttackType2();
            countDown = m_Behavior.MonsterStat.MonsterStat.baseCoolDown;
        }
    }
    void AttackType1()
    {
        GameObject fireOrb = MageProjectilePool.ins.GetObject();
        fireOrb.transform.position=fireBallHolder.transform.position;
        fireOrb.GetComponent<ProjectileMove>().GetDirection(transform.forward,transform.rotation);
        fireOrb.GetComponent<FireBallDetection>().Damage = m_Monster.MonsterStat.damage;
    }
    void AttackType2()
    {
        //Debug.Log("Attack2");
    }
    int RandomWithProbability()
    {
        float rand = Random.Range(0, 1f);
        if (rand < 0.2f)
            return 1;
        else return 2;
    }
}
