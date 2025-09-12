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
    float radius;
    float moveTime = .8f;
    public BossRandMove(NavMeshAgent _agent, BaseChar baseChar, BossMoveHandler handler, BaseMonster monster)
    {
        agent = _agent;
        currentChar = baseChar;
        currentHandler = handler;
        thisMonster = monster;
    }
    public void Enter()
    {

    }
    public void Execute()
    {
        moveTime-=Time.deltaTime;
        if (moveTime < 0) { 

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
