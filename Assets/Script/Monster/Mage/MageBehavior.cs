using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;


public class MageBehavior : MonsterBehavior
{
    MageMoveHandler mageMoveHandler;
   
    protected override void Start()
    {
        base.Start();
        mageMoveHandler = new MageMoveHandler(agent,player,monsterstat);
    }

    // Update is called once per frame
    void Update()
    {
        mageMoveHandler.HandleMoveState();       
    }

   

    
}
