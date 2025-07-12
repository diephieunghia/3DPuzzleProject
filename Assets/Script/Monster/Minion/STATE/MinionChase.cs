using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MinionChase : IState
{
    NavMeshAgent agent;
    BaseChar currentChar;
    MinionMoveHandler currentHandler;
    

    public MinionChase(NavMeshAgent _agent,BaseChar character, MinionMoveHandler handler)
    {
        agent = _agent;
        currentChar = character;
        currentHandler = handler;
        
    }
    public void Enter()
    {
        
    }

    public void Execute()
    {
        agent.SetDestination(currentChar.gameObject.transform.position);
        if (agent.remainingDistance <= 0)
        {
            Debug.Log("prepare to attack");
        }
        if (currentChar.IsShooting())
        {
            
        }
    }

    public void Exit()
    {
        
    }
}
