using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(ChangeDate))]
public class GameManager : MonoBehaviour
{
    ChangeDate changeDate;

    public static GameManager ins { get; private set; }
    [SerializeField] SO_Level statScale;
    
    public int CurrentLevel=>statScale.currentLevel;
    public int MaxLevel => statScale.maxLevel;
    public int[] MaxEnemy => statScale.maxEnemyPerLevel;
    public float MonsterScale => statScale.monsterPowerScale;
    public float[] LevelTime=>statScale.levelTime;
    public float[] SpawnRate => statScale.spawnRate;
    public int[] SpawnQuantity=>statScale.spawnQuantity;

    public int monsterOnFieldCount=0;
    //level
    public Action<float,float> LevelChange;
    //CountDown
    public Action CountDownComplete;
    public Action MoveToArea;
    public Action<ArcherBlackBoard> GetCharStat;
    //Move to store
    public Transform storePosition;
    ArcherAction character;
    public bool playerPressBuy = false;
    public bool playerExitStore = false;
    //last character position
    Vector3 lastPosition;

    //wait time
    public float WaitTime = .5f;

    private void Awake()
    {
        if (ins != null && ins != this)
            Destroy(this);
        else
            ins = this;
        changeDate = GetComponent<ChangeDate>();
        changeDate.ChangeSkyBox(0);

        CountDownComplete += MoveToStore;
        CountDownComplete += IncreaseCurrentLevel;
        MoveToArea += MoveBackToArea;
    }
    private void Start()
    {
        character=GameObject.FindAnyObjectByType<ArcherAction>();
        Application.targetFrameRate = 60;
    }
    public void StatLevelUp(ArcherBlackBoard stat)
    {

    }

    //Move to store
    private void MoveToStore()
    {
        StartCoroutine(WaitAndMove());
    }
    IEnumerator WaitAndMove()
    {
        yield return new WaitForSeconds(0.75f);
        //Move to store method
        character.characterController.enabled = false;
        lastPosition=character.transform.position;
        character.transform.position = storePosition.transform.position;
        character.characterController.enabled = true;
        yield return new WaitForSeconds(0.5f);
        //change date
        if(CurrentLevel%2==0)
        {
            changeDate.ChangeSkyBox(CurrentLevel/2);
        }
    }
    public void MoveBackToArea()
    {
        StartCoroutine(MoveBack());
    }
    IEnumerator MoveBack()
    {
        //play vfx
        yield return new WaitForSeconds(WaitTime/2);
        //Move to store method
        character.characterController.enabled = false;
        character.transform.position = lastPosition;
        character.characterController.enabled = true;
    }
    public void IncreaseCurrentLevel()
    {
        if (statScale.currentLevel <= statScale.maxLevel)
            statScale.currentLevel += 1;
        SpawnMonster.ins.MaxEnemy = statScale.maxEnemyPerLevel[statScale.currentLevel-1];
        SpawnMonster.ins.CurrentLevel=statScale.currentLevel;
        SpawnMonster.ins.SpawnRate = statScale.spawnRate[statScale.currentLevel-1];
        SpawnMonster.ins.MonsterQuanity = statScale.spawnQuantity[statScale.currentLevel-1];
        //Debug.Log("Current Level: "+statScale.currentLevel+" max Enemy: "+statScale.maxEnemyPerLevel);
    }
}
