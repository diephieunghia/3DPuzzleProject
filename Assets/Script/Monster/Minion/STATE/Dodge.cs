using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dodge : IState
{
    NavMeshAgent agent;
    BaseChar currentChar;
    MinionMoveHandler currentHandler;
    BaseMonster monsterStat;

    float radius = 2f;
    Vector3 randomPoint;

    public Dodge(NavMeshAgent _agent, BaseChar character, MinionMoveHandler handler, BaseMonster stat)
    {
        agent = _agent;
        currentChar = character;
        currentHandler = handler;
        monsterStat = stat;

    }
    public void Enter()
    {
        monsterStat.Attack = false;
        randomPoint = RandomPointInArcLeftRightMonster();
    }

    public void Execute()
    {

    }

    public void Exit()
    {

    }
    
    Vector3 RandomPointInArcLeftRightMonster()
    {
        Vector3 right = agent.transform.right;
        Vector3 left= -agent.transform.right;

        Vector3 dodgeDirection=right;
        int rand = Random.Range(0, 2); // 0 for left, 1 for right
        if(rand==0)
            dodgeDirection = left;

        float angle = Random.Range(-45, 45);

        Quaternion rotation = Quaternion.Euler(0, angle, 0);
        Vector3 direction = rotation * dodgeDirection;

        Vector3 position = agent.transform.position + direction.normalized * radius;
        return position;
    }
}
