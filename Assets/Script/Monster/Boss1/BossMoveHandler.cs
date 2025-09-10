using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMoveHandler : MoveHandler
{
    public BossChase bossChase;
    public BossKeepDistance bossKeepDistance;

    public BossMoveHandler(NavMeshAgent _agent, BaseChar character,BaseMonster monster)
    {
        agent = _agent;
        activeChar = character;
        this.monster = monster;
        bossChase = new BossChase(_agent, character, this,monster);
        bossKeepDistance=new BossKeepDistance(_agent,character,this,monster);
        CurrentState = bossChase;

    }
}
