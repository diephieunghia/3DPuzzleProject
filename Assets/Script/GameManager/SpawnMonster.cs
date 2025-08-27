using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.AI;

public class SpawnMonster : MonoBehaviour
{
    public static SpawnMonster ins;
    bool spawn = false;
    [SerializeField] ObjectPool[] monstersPool;
    //max enemy
    int maxEnemy;
    public int MaxEnemy {  get { return maxEnemy; }  set { maxEnemy = value; } }
    //current level
    int currentLevel;
    public int CurrentLevel {  get { return currentLevel; } set { currentLevel = value; } }
    //spawn rate
    float spawnRate;
    float tempSpawnRate;
    public float SpawnRate { get { return spawnRate; } set { spawnRate = value; } }
    //monster spawn per rate
    int monsterQuanity;
    public int MonsterQuanity { get { return monsterQuanity; } set { monsterQuanity = value; } }

    //spawn Location
    [SerializeField] GameObject[] spawnPos;
    int spawnRarity;
    float maxNavMeshDistance = 4f;
    private void Awake()
    {
        if (ins != null && ins != this)
            Destroy(this);
        else
            ins = this;
    }

    //get new spawn rate and spawn quantity after start a new wave
    void Start()
    {
        StartCoroutine(WaitToSpawn());
        spawnRate = 3f;
        tempSpawnRate = 3f;
        spawn = true;
        GameManager.ins.CountDownComplete += StopSpawnAndDespawnMonster;
        GameManager.ins.MoveToArea += LevelStart;
        monsterQuanity=GameManager.ins.SpawnQuantity[GameManager.ins.CurrentLevel-1];
        maxEnemy = GameManager.ins.MaxEnemy[GameManager.ins.CurrentLevel-1];
    }

    void Update()
    {
        if (spawn)
        {
            tempSpawnRate -= Time.deltaTime;
            if (tempSpawnRate <= 0)
            {
                spawnRarity = GetSpawnRarity();
                Debug.Log("Spawn Monster");
                SpawnMonsterMethod(spawnRarity);
                tempSpawnRate = spawnRate;
            }
        }
        
    }
    void LevelStart()
    {
        //reset monster onfield count
        GameManager.ins.monsterOnFieldCount = 0;
        StartCoroutine(WaitToSpawn());
    }
    IEnumerator WaitToSpawn()
    {
        yield return new WaitForSeconds(GameManager.ins.WaitTime);
        spawn = true;
        
        spawnRate = GameManager.ins.SpawnRate[GameManager.ins.CurrentLevel - 1];
        tempSpawnRate = 0;
    }
    Vector3 GetRandomSpawnPoint()
    {
        int rand = Random.Range(0, spawnPos.Length - 2);
        Vector3 size = spawnPos[rand].GetComponent<BoxCollider>().size;
        Vector3 halfSize = size * .5f;
        float x=Random.Range(-halfSize.x, halfSize.y);
        float z=Random.Range(-halfSize.z, halfSize.z);
        //convert to world position
        Vector3 randomLocation=new Vector3(x,halfSize.y,z)+spawnPos[rand].transform.position;
        if (NavMesh.SamplePosition(randomLocation, out NavMeshHit hit, maxNavMeshDistance, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return randomLocation;

    }
    void SpawnMonsterMethod(int rarity)
    {
        //get monster type and then spawn a batch of them
        for (int i = 0; i < monsterQuanity; i++)
        {          
            //check if exceed max enemy per level
            if (GameManager.ins.monsterOnFieldCount <= maxEnemy)
            {
                //get monster from object pool               
                GameObject monster = monstersPool[rarity].GetObject();
                //assign level scale to monster
                monster.GetComponent<BaseMonster>()?.IncreaseStatWithLevel(GameManager.ins.CurrentLevel);
                //assign spawn location
                monster.transform.position=GetRandomSpawnPoint();
                //increase count to game manager
                GameManager.ins.monsterOnFieldCount++;
            }
        }
    }
    void StopSpawnAndDespawnMonster()
    {
        spawn = false;
    }
    int GetSpawnRarity()
    {
        if (currentLevel <= 2)
            return 0;
        else if (3 <= currentLevel && currentLevel <= 5)
        {
            float r = Random.Range(0f, 1f);
            if (r < 0.4f)
                return 0;
            else return 1;
        }
        else if(3<=currentLevel&& currentLevel<=7)
        {
            float r = Random.Range(0f, 1f);
            if (r <= 0.1) return 0;
            else if (0.1 < r && r <= 0.7) return 1;
            else return 2;
        }
        else
        {
            float r = Random.Range(0f, 1f);
            if (r <= 0.1) return 0;
            else if (0.1 < r && r <= 0.55) return 1;
            else return 2;
        }

        
    }
}
