using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomMove : IState
{
    NavMeshAgent agent;
    BaseChar currentChar;
    MoveHandler currentHandler;

    public RandomMove(NavMeshAgent _agent, BaseChar baseChar, MoveHandler handler)
    {
        agent = _agent;
        currentChar = baseChar;
        this.currentHandler = handler;
    }
    public void Enter()
    {

    }

    public void Execute()
    {
        float deltaDistance = agent.remainingDistance - agent.stoppingDistance;       
        if (deltaDistance >= -.3f)
            currentHandler.MoveChangeState(currentHandler.chase);


    }

    public void Exit() { }
}
