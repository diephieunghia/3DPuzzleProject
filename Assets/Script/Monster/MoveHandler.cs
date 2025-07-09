using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveHandler  
{
    public IState CurrentState;
    public NavMeshAgent agent;
   
      
    protected BaseChar activeChar;

    public void MoveChangeState(IState nextState)
    {
        CurrentState.Exit();
        CurrentState = nextState;
        CurrentState.Enter();
    }
    public void HandleMoveState()
    {
        CurrentState.Execute();
    }
}
