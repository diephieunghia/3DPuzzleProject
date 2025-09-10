using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonsterBehavior
{
    BossMoveHandler bossMoveHandler;

    protected override void Start()
    {
        base.Start();
        agent.speed = monsterstat.MonsterStat.speed;
        bossMoveHandler = new BossMoveHandler(agent,player,monsterstat);
    }

    private void Update()
    {
        if (!monsterstat.Death)
            bossMoveHandler.HandleMoveState();
    }


}
