using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MageBehavior : MonoBehaviour
{
    protected NavMeshAgent agent;
    protected BaseChar player;

    MoveHandler moveHandler;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<BaseChar>();
        moveHandler=new MoveHandler(agent,player);
    }

    // Update is called once per frame
    void Update()
    {
        moveHandler.HandleMoveState();
        
    }
}
