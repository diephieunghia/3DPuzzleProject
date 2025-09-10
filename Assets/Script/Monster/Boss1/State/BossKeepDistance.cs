using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossKeepDistance : IState
{
    NavMeshAgent agent;
    BaseChar currentChar;
    BossMoveHandler currentHandler;
    BaseMonster thisMonster;

    Vector3 randomPoint;
    float radius = 10f;
    Vector3 deltaDistance;
    float updateTime = 2f;
    float tempStopping;
    public BossKeepDistance(NavMeshAgent _agent, BaseChar baseChar, BossMoveHandler handler, BaseMonster monster)
    {
        agent = _agent;
        currentChar = baseChar;
        currentHandler = handler;
        thisMonster = monster;
    }
    public void Enter()
    {
        if (thisMonster.Death)
            agent.isStopped = true;
        randomPoint = RandomPointInArcBehindMonster();
        updateTime = 0f;
        tempStopping=agent.stoppingDistance; 
    }

    public void Execute()
    {
        updateTime -= Time.deltaTime;
        deltaDistance = currentChar.gameObject.transform.position - agent.transform.position;
        agent.transform.LookAt(currentChar.gameObject.transform.position);
        if (deltaDistance.magnitude > tempStopping*1.5f)
        {
            agent.updateRotation = true;
            agent.stoppingDistance = tempStopping;
            currentHandler.MoveChangeState(currentHandler.bossChase);
        }
        if (deltaDistance.magnitude<12.5f && updateTime <= 0)
        {
            randomPoint = RandomPointInArcBehindMonster();
            updateTime = 2f;
        }
        if (deltaDistance.magnitude <= tempStopping / 2)
        {
            agent.stoppingDistance = 0.5f;
            agent.SetDestination(randomPoint);
        }
        
                   
    }

    public void Exit()
    {
        updateTime = 2f;
    }
    Vector3 RandomPointInArcBehindMonster()
    {
        Vector3 back = -agent.transform.forward;

        float angle = Random.Range(-30, 30);

        Quaternion rotation = Quaternion.Euler(0, angle, 0);
        Vector3 direction = rotation * back;

        Vector3 position = agent.transform.position + direction.normalized * radius;
        return position;
    }
}
