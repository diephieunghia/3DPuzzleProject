using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossRandMove : IState
{
    NavMeshAgent agent;
    BaseChar currentChar;
    BossMoveHandler currentHandler;
    BaseMonster thisMonster;

    Vector3 deltaDistance;
    float tempStopping;
    float radius;
    float moveTime = 2f;
    //int check if destination is point or char
    int check = 2;
    public BossRandMove(NavMeshAgent _agent, BaseChar baseChar, BossMoveHandler handler, BaseMonster monster)
    {
        agent = _agent;
        currentChar = baseChar;
        currentHandler = handler;
        thisMonster = monster;
    }
    public void Enter()
    {
        tempStopping=agent.stoppingDistance;
        Debug.Log("BossRandMove");
        agent.updateRotation = false;
    }
    public void Execute()
    {
        moveTime-=Time.deltaTime;
        NavMeshHit hit;
        if (moveTime < 0&&check==2) { 
            if(NavMesh.SamplePosition(RandomPointInArcBehindMonster(),out hit, 1f, NavMesh.AllAreas))
            {
                agent.stoppingDistance = 0.2f;
                agent.SetDestination(hit.position);
                check = 1;
            }
        }
        //check if agent reach point destination to switch back to chasing character
        if(check==1&& agent.remainingDistance <= 0.2f)
        {
            check = 2;
            agent.stoppingDistance = tempStopping;
            moveTime = .8f;
            agent.SetDestination(currentChar.transform.position);
            if (agent.remainingDistance > tempStopping)
                currentHandler.MoveChangeState(currentHandler.bossChase);
            else if (agent.remainingDistance <= tempStopping / 2)
                currentHandler.MoveChangeState(currentHandler.bossKeepDistance);            
        }

    }
    public void Exit()
    {

    }
    Vector3 RandomPointInArcBehindMonster()
    {
        Vector3 back = -agent.transform.forward;

        float angle = Random.Range(-90, 90);

        Quaternion rotation = Quaternion.Euler(0, angle, 0);
        Vector3 direction = rotation * back;
        radius = Random.Range(thisMonster.MonsterStat.randomRadius1, thisMonster.MonsterStat.randomRadius2);
        Vector3 position = agent.transform.position + direction.normalized * radius;
        return position;
    }
}
