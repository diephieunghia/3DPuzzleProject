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
        agent.SetDestination(currentChar.gameObject.transform.position);
        thisMonster.transform.LookAt(currentChar.transform.position);
        if (agent.remainingDistance <=agent.stoppingDistance/2)
            currentHandler.MoveChangeState(currentHandler.bossKeepDistance);
        else if(agent.stoppingDistance/2<agent.remainingDistance&&agent.remainingDistance<=agent.stoppingDistance)
            currentHandler.MoveChangeState(currentHandler.bossRandMove);
        
    }

    public void Exit() {
        
    }
}
