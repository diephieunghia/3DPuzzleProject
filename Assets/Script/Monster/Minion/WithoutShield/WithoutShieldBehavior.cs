using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WithoutShieldBehavior : MonsterBehavior
{
    WithoutShieldMoveHandler withoutShieldMoveHandler;
    protected override void Start()
    {
        base.Start();
        withoutShieldMoveHandler=new WithoutShieldMoveHandler(agent,player,monsterstat);
    }
    private void Update()
    {
        if (!monsterstat.Death) {
            withoutShieldMoveHandler.HandleMoveState();
        }
    }
}