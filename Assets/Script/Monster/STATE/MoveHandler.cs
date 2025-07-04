using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveHandler  
{
    public IState CurrentState;
    public NavMeshAgent agent;
    BaseChar activeChar;

    public Chase chase;
    public RandomMove randMove;
    public MoveHandler(NavMeshAgent _agent,BaseChar character)
    {
        agent = _agent;
        activeChar= character;
        chase = new Chase(_agent, character,this);
        randMove = new RandomMove(_agent, character, this);
        CurrentState = chase;
    }

    public void MoveChangeState(IState nextState)
    {
        CurrentState.Exit();
        CurrentState = nextState;
        CurrentState.Enter();
    }
    public void HandleAttackState()
    {
        CurrentState.Execute();
    }
}
