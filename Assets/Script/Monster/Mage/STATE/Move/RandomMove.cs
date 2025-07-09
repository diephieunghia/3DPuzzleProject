using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomMove : IState
{
    NavMeshAgent agent;
    BaseChar currentChar;
    MageMoveHandler currentHandler;

    float radius = 2f;
    float moveInterval = .7f;

    float timer;
    public RandomMove(NavMeshAgent _agent, BaseChar baseChar, MageMoveHandler handler)
    {
        agent = _agent;
        currentChar = baseChar;
        this.currentHandler = handler;
    }
    public void Enter()
    {
        timer = moveInterval;
    }

    public void Execute()
    {
        Vector3 deltaDistance=currentChar.gameObject.transform.position-agent.transform.position;
        if (deltaDistance.magnitude > 10f)
        {
            agent.updateRotation = true;
            agent.stoppingDistance = 8;
            agent.acceleration = 8;
            currentHandler.MoveChangeState(currentHandler.chase);           
        }
        else if (deltaDistance.magnitude < 7)
        {
            agent.stoppingDistance = Random.Range(8, 10);
            agent.acceleration = 8;
            currentHandler.MoveChangeState(currentHandler.keepDistance);            
        }
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Vector3 randomPoint = RandomPointInCircle();
            NavMeshHit hit;

            if (NavMesh.SamplePosition(randomPoint, out hit, .5f, NavMesh.AllAreas))
            {
                agent.stoppingDistance = 1;
                agent.acceleration = 30f;               
                agent.SetDestination(hit.position);
                agent.updateRotation = false;
                agent.transform.rotation = Quaternion.LookRotation(deltaDistance);
            }
            timer = moveInterval;
        }
        

    }

    public void Exit() {
       
    }

    Vector3 RandomPointInCircle()
    {
        float angle = Random.Range(0f, Mathf.PI * 2);
        float xOffset = Mathf.Cos(angle) * radius;
        float zOffset = Mathf.Sin(angle) * radius;

        return agent.transform.position + new Vector3(xOffset, 0f, zOffset);
    }
}
