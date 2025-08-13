using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WithoutShieldMoveHandler : MoveHandler
{
    public ChaseWithoutShield withoutShield;

    public WithoutShieldMoveHandler(NavMeshAgent _agent, BaseChar charController, BaseMonster stat)
    {
        agent = _agent;
        activeChar = charController;
        withoutShield = new ChaseWithoutShield(agent, activeChar, this, stat);
        CurrentState=withoutShield;
        monster = stat;
    }
}
