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

    float enableBoxCastTime = 10f;
    float boxCoolDown = 2f;

    Collider[] overlapVision;
    public MinionChase(NavMeshAgent _agent,BaseChar character, MinionMoveHandler handler,BaseMonster stat)
    {
        agent = _agent;
        currentChar = character;
        currentHandler = handler;
        monsterStat = stat;
        
    }
    public void Enter()
    {
        boxCoolDown = 2f;
        enableBoxCastTime = 3f;
        agent.speed = monsterStat.MonsterStat.speed;
        if (monsterStat.Death) agent.isStopped = true;
    }

    public void Execute()
    {
        monsterStat.CoolDown -= Time.deltaTime;
        boxCoolDown-= Time.deltaTime;
        agent.SetDestination(currentChar.gameObject.transform.position);
        agent.transform.LookAt(currentChar.gameObject.transform.position);
        //Debug.Log(agent.remainingDistance);
        if (agent.remainingDistance <= 3.6f&&monsterStat.CoolDown<=0)
        {
            monsterStat.Attack = true;
            monsterStat.attackType = Random.Range(1, 3); // Randomly choose attack type between 1 and 2           
            monsterStat.CoolDown = monsterStat.MonsterStat.baseCoolDown; // Reset cooldown to base cooldown time           
        }
        else if(agent.remainingDistance>3.6f)
        {
            monsterStat.Attack = false;
        }
        if (boxCoolDown <= 0) {
            enableBoxCastTime-=Time.deltaTime;
            overlapVision = Physics.OverlapBox(monsterStat.center.position, monsterStat.size / 2, monsterStat.transform.rotation, monsterStat.arrow);
            foreach(Collider overlap in overlapVision)
            {
                if (overlap.CompareTag("Projectile")&&overlap.GetComponentInParent<Rigidbody>()!=null)
                {
                    if (overlap.GetComponentInParent<Rigidbody>().velocity.sqrMagnitude>1)
                    {
                        currentHandler.MoveChangeState(currentHandler.dodge);
                    }
                   
                }
            }
            if(enableBoxCastTime<0)
            {
                boxCoolDown = 2f;
                enableBoxCastTime = 10f;
            }
        }
    }

    public void Exit()
    {
        
    }
}
