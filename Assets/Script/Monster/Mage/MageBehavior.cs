using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MageBehavior : MonsterBehavior
{   
    MageMoveHandler mageMoveHandler;
    protected override void Start()
    {
        base.Start();
        mageMoveHandler = new MageMoveHandler(agent,player);
    }

    // Update is called once per frame
    void Update()
    {
        mageMoveHandler.HandleMoveState();       
    }
}
