using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class UICountDown : MonoBehaviour
{  
    //countdown and handle countdown complete, start again
    float timeCounter;
    int currentLevel = 1;

    float minutes;
    float seconds;

    bool countDownComplete = true;

    public TextMeshProUGUI wave;
    private void Start()
    {
        currentLevel= GameManager.ins.CurrentLevel ;
        wave.text = string.Format("Wave {0}", GameManager.ins.CurrentLevel);
        timeCounter = GameManager.ins.LevelTime[currentLevel - 1];
        //Set time at the beginning
        timeCounter -= Time.deltaTime;
        minutes = Mathf.FloorToInt(timeCounter / 60f);
        seconds = Mathf.FloorToInt(timeCounter - minutes * 60);
        UIManager.ins.timeCountDown.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        //register event
        GameManager.ins.MoveToArea += ResetUpdateVar;
        //delay before enter combat
        StartCoroutine(StartNewStage());
    }
    private void Update()
    {
        if (!countDownComplete)
        { 
            countDown();
            //Update UI match time countdown
            UIManager.ins.timeCountDown.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            
        }       
        
    }
    void countDown()
    {
        if (timeCounter <= 0)
        { 
            countDownComplete = true;
            minutes = 0;
            seconds = 0;
            //Invoke Despawn Monster, Inovke transport, Invoke change Character State
            GameManager.ins.CountDownComplete?.Invoke();
            return;
        }
        timeCounter -= Time.deltaTime;
        minutes=Mathf.FloorToInt(timeCounter / 60f);
        seconds = Mathf.FloorToInt(timeCounter - minutes * 60);
    }
    void ResetUpdateVar()
    {
        StartCoroutine(StartNewStage());
    }
    IEnumerator StartNewStage()
    {
        yield return new WaitForSeconds(GameManager.ins.WaitTime);
        countDownComplete = false;
        wave.text = string.Format("Wave {0}", GameManager.ins.CurrentLevel);
        currentLevel =GameManager.ins.CurrentLevel;
        timeCounter = GameManager.ins.LevelTime[GameManager.ins.CurrentLevel - 1];
    }

    
}
