using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : IState
{
    NavMeshAgent agent;
    BaseChar currentChar;
    MageMoveHandler currentHandler;
    BaseMonster thisMonster;
    public Chase(NavMeshAgent _agent,BaseChar baseChar,MageMoveHandler handler,BaseMonster monster)
    {
        agent = _agent;
        currentChar = baseChar;
        currentHandler = handler;
        thisMonster = monster;  
    }
    public void Enter() {
        //get velocity
        thisMonster.MonsterStat.velocity= agent.speed;
        thisMonster.Attack = false;
    }

    public void Execute() {
       
        float deltaDistance = agent.remainingDistance - agent.stoppingDistance;
        if (0f < deltaDistance && deltaDistance < .5f)
            agent.acceleration = 2f;
        else if (-.7f < deltaDistance && deltaDistance <=-.3f)
        {
            thisMonster.Attack = true;
            currentHandler.MoveChangeState(currentHandler.randMove);            
        }            
        //Debug.Log("stop-remaing: "+(agent.remainingDistance- agent.stoppingDistance));
        agent.SetDestination(currentChar.gameObject.transform.position);
    }

    public void Exit() { }

}
