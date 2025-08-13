using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MinionMoveHandler : MoveHandler
{
    public MinionChase minionChase;
    public Dodge dodge;

    public MinionMoveHandler(NavMeshAgent _agent,  BaseChar charController,BaseMonster stat)
    {
        agent = _agent;
        activeChar = charController;
        minionChase = new MinionChase(agent,activeChar, this,stat);
        dodge=new Dodge(agent,activeChar,this,stat);
        CurrentState = minionChase;
        monster = stat;
        //set agent speed
        agent.speed = monster.MonsterStat.speed;
    }
}
