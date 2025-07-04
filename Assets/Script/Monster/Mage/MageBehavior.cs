using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MageBehavior : MonoBehaviour
{
    protected NavMeshAgent agent;
    protected BaseChar player;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<BaseChar>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.gameObject.transform.position);
    }
}
