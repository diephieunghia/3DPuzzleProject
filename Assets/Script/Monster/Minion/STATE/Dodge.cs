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

    float radius = 3f;
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
        if (monsterStat.Death) agent.isStopped = true;
        monsterStat.Attack = false;
        randomPoint = RandomPointInArcLeftRightMonster();

        Vector3 deltaDistance = currentChar.transform.position - monsterStat.transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, .5f, NavMesh.AllAreas))
        {
            agent.stoppingDistance = 2;
            agent.speed = 50;
            agent.acceleration = 30f;
            agent.SetDestination(hit.position);
            agent.updateRotation = false;
            agent.transform.rotation = Quaternion.LookRotation(deltaDistance);
        }
        else
        {
            currentHandler.MoveChangeState(currentHandler.minionChase);
        }
        
    }

    public void Execute()
    {
             
        if (agent.remainingDistance<=2f)
        {
            currentHandler.MoveChangeState(currentHandler.minionChase);
        }

    }

    public void Exit()
    {
        agent.speed = monsterStat.MonsterStat.speed;
        agent.acceleration = 10f;
        agent.stoppingDistance = 3f;
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
