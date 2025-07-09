using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MinionMoveHandler : MoveHandler
{
    public MinionChase minionChase;

    public MinionMoveHandler(NavMeshAgent _agent,  BaseChar charController)
    {
        agent = _agent;
        activeChar = charController;
        minionChase = new MinionChase(agent,activeChar, this);
        CurrentState = minionChase;
    }
}
