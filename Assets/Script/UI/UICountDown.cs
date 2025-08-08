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

    bool countDownComplete = false;

    public TextMeshProUGUI wave;
    private void Start()
    {
        wave.text = string.Format("Wave {0}", GameManager.ins.CurrentLevel);
        timeCounter = GameManager.ins.LevelTime[currentLevel - 1];
        GameManager.ins.MoveToArea += ResetUpdateVar;
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
        yield return new WaitForSeconds(.75f);
        countDownComplete = false;
        wave.text = string.Format("Wave {0}", GameManager.ins.CurrentLevel);
        currentLevel =GameManager.ins.CurrentLevel;
        timeCounter = GameManager.ins.LevelTime[GameManager.ins.CurrentLevel - 1];
        Debug.Log(timeCounter);
    }
    
}
