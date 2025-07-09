using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MageMoveHandler : MoveHandler
{
    public Chase chase;
    public RandomMove randMove;
    public KeepDistance keepDistance;

    public MageMoveHandler(NavMeshAgent _agent, BaseChar character)
    {
        agent = _agent;
        activeChar = character;
        chase = new Chase(_agent, character, this);
        randMove = new RandomMove(_agent, character, this);
        keepDistance = new KeepDistance(_agent, character, this);
        CurrentState = chase;
    }
}
