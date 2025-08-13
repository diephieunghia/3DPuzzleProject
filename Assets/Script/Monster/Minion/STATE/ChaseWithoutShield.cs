using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseWithoutShield : IState
{
    NavMeshAgent agent;
    BaseChar currentChar;
    WithoutShieldMoveHandler currentHandler;
    BaseMonster monsterStat;

    public ChaseWithoutShield(NavMeshAgent _agent, BaseChar character, WithoutShieldMoveHandler handler, BaseMonster stat)
    {
        agent = _agent;
        currentChar = character;
        currentHandler = handler;
        monsterStat = stat;
    }
    public void Enter()
    {

        if (monsterStat.Death) agent.isStopped = true;
    }
    public void Execute()
    {
        monsterStat.CoolDown -= Time.deltaTime;
        agent.SetDestination(currentChar.gameObject.transform.position);
        agent.transform.LookAt(currentChar.gameObject.transform.position);
        //Debug.Log(agent.remainingDistance);
        if (agent.remainingDistance <= 3.6f && monsterStat.CoolDown <= 0)
        {
            monsterStat.Attack = true;
            monsterStat.attackType = Random.Range(1, 3); // Randomly choose attack type between 1 and 2           
            monsterStat.CoolDown = monsterStat.MonsterStat.baseCoolDown; // Reset cooldown to base cooldown time           
        }
        else if (agent.remainingDistance > 3.6f)
        {
            monsterStat.Attack = false;
        }
    }
    public void Exit()
    {

    }
}
