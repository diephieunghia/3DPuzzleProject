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
    public KeepDistance keepDistance;
    public MoveHandler(NavMeshAgent _agent,BaseChar character)
    {
        agent = _agent;
        activeChar= character;
        chase = new Chase(_agent, character,this);
        randMove = new RandomMove(_agent, character, this);
        keepDistance=new KeepDistance(_agent, character, this);
        CurrentState = chase;
    }

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
