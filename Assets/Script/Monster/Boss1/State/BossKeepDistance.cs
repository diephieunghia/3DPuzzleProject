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
    int check = 2;
    float tempRemaing;
    public BossKeepDistance(NavMeshAgent _agent, BaseChar baseChar, BossMoveHandler handler, BaseMonster monster)
    {
        agent = _agent;
        currentChar = baseChar;
        currentHandler = handler;
        thisMonster = monster;
    }
    public void Enter()
    {
        Debug.Log("Boss Keep Distance");
        if (thisMonster.Death)
            agent.isStopped = true;
        randomPoint = RandomPointInArcBehindMonster();
        updateTime = 0f;
        tempStopping=agent.stoppingDistance; 
    }

    public void Execute()
    {
        updateTime -= Time.deltaTime;
        agent.transform.LookAt(currentChar.gameObject.transform.position);

        if (check == 2)
        {
            tempRemaing = agent.remainingDistance;
            if (agent.remainingDistance > tempStopping )
            {
                agent.updateRotation = true;
                agent.stoppingDistance = tempStopping;
                currentHandler.MoveChangeState(currentHandler.bossChase);
            }
            else currentHandler.MoveChangeState(currentHandler.bossRandMove);    
            if (tempRemaing < tempStopping / 2 && updateTime <= 0)
            {
                check = 1;
                randomPoint = RandomPointInArcBehindMonster();
                updateTime = 2f;
                agent.stoppingDistance = 0.5f;
                agent.SetDestination(randomPoint);
            }
        }
        else
        {
            if (agent.remainingDistance <= 0f)
            {
                //boss reach short point,change to chase char
                agent.SetDestination(currentChar.transform.position);
                agent.stoppingDistance = tempStopping;
                updateTime = 2f;
            }

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
