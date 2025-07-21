using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionBehavior : MonsterBehavior
{

    MinionMoveHandler moveHandler;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        moveHandler = new MinionMoveHandler(agent,player);
    }

    // Update is called once per frame
    void Update()
    {
        if (!monsterstat.Death)        
            moveHandler.HandleMoveState();

    }
}
