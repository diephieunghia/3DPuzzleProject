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

    float radius = 5f;
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
        agent.speed = 50;
        agent.stoppingDistance = 0;
        agent.SetDestination(randomPoint);
    }

    public void Execute()
    {
        Vector3 deltaDistance=currentChar.transform.position-monsterStat.transform.position;
        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomPoint, out hit, 1f, NavMesh.AllAreas))
        {
            agent.stoppingDistance = 0;
            agent.acceleration = 30f;
            agent.SetDestination(hit.position);
            agent.updateRotation = false;
            agent.transform.rotation = Quaternion.LookRotation(deltaDistance);
        }
        if (agent.remainingDistance<=1f)
        {          
            currentHandler.MoveChangeState(currentHandler.minionChase);
        }

    }

    public void Exit()
    {
        agent.speed = monsterStat.MonsterStat.speed;
        agent.stoppingDistance = 4.8f;
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
