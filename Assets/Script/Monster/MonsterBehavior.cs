using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(BaseMonster))]
public class MonsterBehavior : MonoBehaviour
{
    protected BaseMonster monsterstat;
    public BaseMonster MonsterStat => monsterstat;
    protected NavMeshAgent agent;
    protected BaseChar player;

    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<BaseChar>();       
        monsterstat = GetComponent<BaseMonster>();
    }
    
}
