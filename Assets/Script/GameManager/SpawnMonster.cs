using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class SpawnMonster : MonoBehaviour
{
    public static SpawnMonster ins;
    bool spawn = false;
    [SerializeField] ObjectPool[] monsters;
    int maxEnemy;
    public int MaxEnemy {  get { return maxEnemy; }  set { maxEnemy = value; } }

    [SerializeField] Transform[] spawnPos;
    private void Awake()
    {
        if (ins != null && ins != this)
            Destroy(this);
        else
            ins = this;
    }
    void Start()
    {
        StartCoroutine(WaitToSpawn());
    }

    void Update()
    {
        
    }
    IEnumerator WaitToSpawn()
    {
        yield return new WaitForSeconds(GameManager.ins.WaitTime);
        spawn = true;
    }
}
