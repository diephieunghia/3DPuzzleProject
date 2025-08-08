using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager ins { get; private set; }
    [SerializeField] SO_Level statScale;
    
    public int CurrentLevel=>statScale.currentLevel;
    public int MaxLevel => statScale.maxLevel;
    public float MonsterScale => statScale.monsterPowerScale;
    public float[] LevelTime=>statScale.levelTime;

    //level
    public Action<float> LevelChange;
    //CountDown
    public Action CountDownComplete;
    public Action MoveToArea;
    //Move to store
    public Transform storePosition;
    ArcherAction character;
    public bool playerPressBuy = false;
    public bool playerExitStore = false;

    //last character position
    Vector3 lastPosition;

    private void Awake()
    {
        if (ins != null && ins != this)
            Destroy(this);
        else
            ins = this;
        CountDownComplete += MoveToStore;
        CountDownComplete += IncreaseCurrentLevel;
        MoveToArea += MoveBackToArea;
    }
    private void Start()
    {
        character=GameObject.FindAnyObjectByType<ArcherAction>();
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
    }
    public void MoveBackToArea()
    {
        StartCoroutine(MoveBack());
    }
    IEnumerator MoveBack()
    {
        yield return new WaitForSeconds(0.75f);
        //Move to store method
        character.characterController.enabled = false;
        character.transform.position = lastPosition;
        character.characterController.enabled = true;
    }
    public void IncreaseCurrentLevel()
    {
        if (statScale.currentLevel <= statScale.maxLevel)
            statScale.currentLevel += 1;
    }
}
