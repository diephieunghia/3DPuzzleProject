using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnMonster : MonoBehaviour
{  
    //Belongs to GameManager

    float timeCounter;
    int currentLevel = 1;

    float minutes;
    float seconds;

    bool countDownComplete = false;
    private void Start()
    {
        timeCounter = GameManager.ins.statScale.levelTime[currentLevel - 1];
        Debug.Log(timeCounter);
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
            //Invoke Despawn Monster
            GameManager.ins.CountDownComplete?.Invoke();
            return;
        }
        timeCounter -= Time.deltaTime;
        minutes=Mathf.FloorToInt(timeCounter / 60f);
        seconds = Mathf.FloorToInt(timeCounter - minutes * 60);
    }
    void ResetUpdateVar()
    {
        if(currentLevel<=GameManager.ins.statScale.maxLevel)
        {
            currentLevel += 1;
        }
    }
    
}
