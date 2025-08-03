using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager ins { get; private set; }
    public SO_Level statScale;
    //level
    public Action<float> LevelChange;

    //CountDown
    public Action CountDownComplete;

    //Move to store
    public Transform storePosition;
    ArcherAction character;

    private void Awake()
    {
        if (ins != null && ins != this)
            Destroy(this);
        else
            ins = this;
        CountDownComplete += MoveToStore;
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
        character.transform.position = storePosition.transform.position;
        character.characterController.enabled = true;
        Debug.Log("Move to store");
    }

}
