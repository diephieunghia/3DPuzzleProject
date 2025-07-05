using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : IState
{
    NavMeshAgent agent;
    BaseChar currentChar;
    MoveHandler currentHandler;
    public Chase(NavMeshAgent _agent,BaseChar baseChar,MoveHandler handler)
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
            Debug.Log("Move to random move state");
        }            
        //Debug.Log("stop-remaing: "+(agent.remainingDistance- agent.stoppingDistance));
        agent.SetDestination(currentChar.gameObject.transform.position);
    }

    public void Exit() { }

}
