using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class MonsterBehavior : MonoBehaviour
{
    protected NavMeshAgent agent;
    protected BaseChar player;

    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<BaseChar>();       
    }
    
}
