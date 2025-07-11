using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KeepDistance : IState
{
    NavMeshAgent agent;
    BaseChar currentChar;
    MageMoveHandler currentHandler;
    BaseMonster thisMonster;

    Vector3 randomPoint;
    float radius = 6f;
    Vector3 deltaDistance;
    float updateTime = 3f;
    public KeepDistance(NavMeshAgent _agent, BaseChar baseChar, MageMoveHandler handler,BaseMonster monster)
    {
        agent = _agent;
        currentChar = baseChar;
        currentHandler = handler;
        thisMonster = monster;
    }
    public void Enter() {
        randomPoint=RandomPointInArcBehindMonster();
    }

    public void Execute() {
        updateTime-=Time.deltaTime;
        deltaDistance = currentChar.gameObject.transform.position - agent.transform.position;
        if (deltaDistance.magnitude > 10f)
        {
            agent.updateRotation = true;
            agent.stoppingDistance = 8f;
            thisMonster.MonsterStat.attack = false;
            currentHandler.MoveChangeState(currentHandler.chase);
            
        }
        else if (8f < deltaDistance.magnitude && deltaDistance.magnitude <= 10f)
        {
            agent.stoppingDistance = 8f;
            currentHandler.MoveChangeState(currentHandler.randMove);
        }
        else if(deltaDistance.magnitude<=8&&updateTime<=0)
        {
            agent.transform.LookAt(currentChar.gameObject.transform.position);
            randomPoint = RandomPointInArcBehindMonster();                       
        }

        agent.stoppingDistance = .5f;
        agent.SetDestination(randomPoint); 

    }

    public void Exit() { }

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
