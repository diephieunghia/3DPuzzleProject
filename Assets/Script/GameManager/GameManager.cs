using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(ChangeDate),typeof(AssignCard),typeof(GameCompleteOrDie))]
public class GameManager : MonoBehaviour
{
    ChangeDate changeDate;
    AssignCard assignCard;

    /// <summary>
    /// ----------------Game Complete or Die-----------------
    /// </summary>
    public GameCompleteOrDie completeorDie;
    

    public AssignCard AssignCard => assignCard;
    public static GameManager ins { get; private set; }
    //Level Stat
    [SerializeField] SO_Level statScale;

    int tempCurrentLevel = 1;
    public int CurrentLevel=>tempCurrentLevel;
    public int MaxLevel => statScale.maxLevel;
    public int[] MaxEnemy => statScale.maxEnemyPerLevel;
    public float MonsterScale => statScale.monsterPowerScale;
    public float[] LevelTime=>statScale.levelTime;
    public float[] SpawnRate => statScale.spawnRate;
    public int[] SpawnQuantity=>statScale.spawnQuantity;

    //Monster management
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

    [Header("Shop event")]
    public bool playerPressBuy = false;
    public bool playerPressReset = false;
    public bool playerExitStore = false;
    public float coinsHeld = 0;
    public Action<float> UpdateCoinsAmount;
    public Action BuyItem;
    public Action Reset;
    //reset cost
    float resetCost = 3;
    //last character position
    Vector3 lastPosition;
    //Monster stat increase per level
    public Action monsterStatIncrease;

    //wait time
    public float WaitTime = .5f;

    private void Awake()
    {
        if (ins != null && ins != this)
            Destroy(this);
        else
            ins = this;
        changeDate = GetComponent<ChangeDate>();
        assignCard=GetComponent<AssignCard>();
        completeorDie = GetComponent<GameCompleteOrDie>();
        completeorDie=GetComponent<GameCompleteOrDie>();

        changeDate.ChangeSkyBox(0);

        CountDownComplete += IncreaseCurrentLevel;
        CountDownComplete += MoveToStore;        
        MoveToArea += MoveBackToArea;
        Reset += ResetCard;
    }
    private void Start()
    {
        character=GameObject.FindAnyObjectByType<ArcherAction>();
        Application.targetFrameRate = 60;
    }
    //-------------------------------------------------
    //----------------Count Down Complete--------------
    //Move to store    //transport to store after wave finish
    private void MoveToStore()
    {
        //get reset amount to UI
        UIManager.ins.UpdateResetCost((int)resetCost);
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

    //move back to area where the character last was
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

    //Increase current level after countdown wave complete
    public void IncreaseCurrentLevel()
    {
        tempCurrentLevel = tempCurrentLevel + 1;
        if (tempCurrentLevel <= statScale.maxLevel)
        {
            SpawnMonster.ins.MaxEnemy = statScale.maxEnemyPerLevel[tempCurrentLevel - 1];
            SpawnMonster.ins.CurrentLevel = tempCurrentLevel;
            SpawnMonster.ins.SpawnRate = statScale.spawnRate[tempCurrentLevel - 1];
            SpawnMonster.ins.MonsterQuanity = statScale.spawnQuantity[tempCurrentLevel - 1];
            //find all monster available and increase their stat
            monsterStatIncrease?.Invoke();
        }
        else
            //when level >=10, mark as game complete
            completeorDie.GameCompleteAction?.Invoke(true);
        

        //Debug.Log("Current Level: "+statScale.currentLevel+" max Enemy: "+statScale.maxEnemyPerLevel);
    }

    //-------------------------------------------------
    //-------------------shopManager-------------------
    void ResetCard()
    {
        if (coinsHeld < resetCost)
        {
            Debug.Log("Not enough coins to reset");
            return;
        }
        //reduce coins and update to UI
        coinsHeld-= resetCost;
        UIManager.ins.CoinsChange(-resetCost,coinsHeld);
        //randomize item


        //update reset cost
        //......increase cost.....
        resetCost += resetCost + tempCurrentLevel;
        UIManager.ins.UpdateResetCost((int)Mathf.Round(resetCost));
    }
}
