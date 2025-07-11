using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MageAttack : MonoBehaviour
{
    MageBehavior m_Behavior;

    float coolDown = 3f;
    float countDown = 3f;
    void Start()
    {
        m_Behavior = GetComponent<MageBehavior>();
    }
    // Update is called once per frame
    void Update()
    {
        countDown -= Time.deltaTime;
        if (countDown <= 0 && m_Behavior.MonsterStat.MonsterStat.attack)
        {
            int rand = RandomWithProbability();
            if (rand == 1)
                AttackType1();
            else
                AttackType2();
            countDown = coolDown;
        }
    }
    void AttackType1()
    {
        //Debug.Log("attack 1");
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
