using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MageMoveHandler : MoveHandler
{
    public Chase chase;
    public RandomMove randMove;
    public KeepDistance keepDistance;

    public MageMoveHandler(NavMeshAgent _agent, BaseChar character,BaseMonster monster)
    {
        agent = _agent;
        activeChar = character;
        this.monster = monster;
        chase = new Chase(_agent, character, this,monster);
        randMove = new RandomMove(_agent, character, this,monster);
        keepDistance = new KeepDistance(_agent, character, this,monster);
        CurrentState = chase;
    }
}
