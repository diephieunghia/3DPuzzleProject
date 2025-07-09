using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : IState
{
    NavMeshAgent agent;
    BaseChar currentChar;
    MageMoveHandler currentHandler;
    public Chase(NavMeshAgent _agent,BaseChar baseChar,MageMoveHandler handler)
    {
        agent = _agent;
        currentChar = baseChar;
        currentHandler = handler;
    }
    public void Enter() { 

    }

    public void Execute() {
        float deltaDistance = agent.remainingDistance - agent.stoppingDistance;

        if (-.7f < deltaDistance && deltaDistance < -.3f)
        { 
            currentHandler.MoveChangeState(currentHandler.randMove);            
        }            
        //Debug.Log("stop-remaing: "+(agent.remainingDistance- agent.stoppingDistance));
        agent.SetDestination(currentChar.gameObject.transform.position);
    }

    public void Exit() { }

}
