using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MinionChase : IState
{
    NavMeshAgent agent;
    BaseChar currentChar;
    MinionMoveHandler currentHandler;
    BaseMonster monsterStat;

    public MinionChase(NavMeshAgent _agent,BaseChar character, MinionMoveHandler handler,BaseMonster stat)
    {
        agent = _agent;
        currentChar = character;
        currentHandler = handler;
        monsterStat = stat;
        
    }
    public void Enter()
    {
        
    }

    public void Execute()
    {
        monsterStat.CoolDown -= Time.deltaTime;
        agent.SetDestination(currentChar.gameObject.transform.position);
        agent.transform.LookAt(currentChar.gameObject.transform.position);
        if (agent.remainingDistance <= 3f&&monsterStat.CoolDown<=0)
        {           
            monsterStat.Attack = true;
            monsterStat.attackType = Random.Range(1, 3); // Randomly choose attack type between 1 and 2           
            monsterStat.CoolDown = monsterStat.MonsterStat.baseCoolDown; // Reset cooldown to base cooldown time           
        }
        else if(agent.remainingDistance>3f)
        {
            monsterStat.Attack = false;            
        }
        if (currentChar.IsShooting())
        {

        }
    }

    public void Exit()
    {
        
    }
}
