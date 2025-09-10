using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossChase :IState
{
    NavMeshAgent agent;
    BaseChar currentChar;
    BossMoveHandler currentHandler;
    BaseMonster thisMonster;
    public BossChase(NavMeshAgent _agent, BaseChar baseChar, BossMoveHandler handler, BaseMonster monster)
    {
        agent = _agent;
        currentChar = baseChar;
        currentHandler = handler;
        thisMonster = monster;
    }

    public void Enter() {
    }

    public void Execute() { 
       float deltaDistance = agent.remainingDistance - agent.stoppingDistance;
        if (deltaDistance >= -12.5f)
            currentHandler.MoveChangeState(currentHandler.bossKeepDistance);
        agent.SetDestination(currentChar.gameObject.transform.position);
    }

    public void Exit() { 
    }
}
