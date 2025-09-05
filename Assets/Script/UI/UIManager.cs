using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using System;
[RequireComponent(typeof(SkillManager))]
public class UIManager : MonoBehaviour
{
    public SkillManager skillManager;
    public static UIManager ins { get; private set; }
    [Header("CrossHair")]
    //change to cross when hit
    public Sprite normal;
    public Sprite hitIcon;
    public GameObject crossHair;

    //level ui
    [Header("Level Character Show")]
    public Image levelUI;
    public TextMeshProUGUI levelText;
    public float rate = 10f;
    float lastValue = 0;
    int runCount = 0;

    //Character Health
    [Header("Character Health")]
    public Image health;
    public TextMeshProUGUI healthNumber;

    //CountDown
    [Header("CountDown")]
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI timeCountDown;

    //Tab Table
    public GameObject tabTable;
    public TextMeshProUGUI character;
    public TextMeshProUGUI level;

    [Header("Coins")]
    //Coins show
    public TextMeshProUGUI coinsAmountTotal;
    public TextMeshProUGUI coinsEarned;
    [Header("Reset")]
    public TextMeshProUGUI reset;

    [Header("FlashColor")]
    public Color flashColor;
    public Color originColor;

    [Header("DamageBlood")]
    public Image bloodDamage;
    private void Awake()
    {
        if (ins != null && ins != this)
            Destroy(this);
        else
            ins = this;
                
    }

    //change to cross when hit
    public void ChangeIconDamage()
    {
        crossHair.GetComponent<Image>().sprite = hitIcon;
        StartCoroutine("WaitToReturnCrossHair");

    }
    IEnumerator WaitToReturnCrossHair()
    {
        yield return new WaitForSeconds(.75f);
        crossHair.GetComponent<Image>().sprite = normal;
    }

    public void LevelChange(float value,float maxEXP, int level)
    {
        if (value == maxEXP)
            runCount+=1;
        else
        {
            StartCoroutine(LevelUp(value, maxEXP, level));
        }                          
    }

    IEnumerator LevelUp(float value,float maxEP, int level)
    {     
        while (runCount >=0 )
        {
            float tempvalue = value;
            if (runCount > 0)
            {
                tempvalue = maxEP;
                lastValue = 0;
            }
            float tempTime = 0;
            while (tempTime <= 1)
            {
                levelUI.fillAmount = Mathf.Lerp(lastValue, tempvalue, tempTime) / maxEP;
                tempTime += Time.deltaTime * rate;
                if (levelUI.fillAmount >= 1)
                {
                    levelUI.fillAmount = 0;
                    lastValue = 0;
                }
                yield return null;
            }
            lastValue = tempvalue;
            levelText.text = string.Format("Level: {0}", level-runCount);
            runCount-=1;
        }
        runCount += 1;
        yield return null;
        
        
    }

    public void TabShow(ArcherBlackBoard bb,bool active)
    {
        if (active)
        {
            tabTable.SetActive(true);
            character.text = string.Format(
                "Max EXP:{0}\r\n" +
                "Health:{1}\r\n" +
                "Armor:{2}\r\n" +
                "Speed:{3}\r\n" +
                "Arrow Count:{4}\r\n" +
                "Max Damage:{5}\r\n"+
                "DashCoolDown:{6}\r\n"+
                "ECoolDown:{7}", bb.maxEXP,bb.health,bb.armor, bb.speed, bb.arrowCount, bb.maxDamage,bb.dashCoolDown,bb.eCoolDown);
            //get statscale from game manager
            level.text = string.Format("Level Scale:{0}", GameManager.ins.MonsterScale);
        }
        else
            tabTable.SetActive(false);
    }
    //change coins text UI
    public void CoinsChange(float current,float total)
    {
        int currencyRound=Mathf.FloorToInt(current);
        coinsEarned.enabled = true;
        if(currencyRound>=0)
            coinsEarned.text = string.Format("+{0}", currencyRound);
        else
            coinsEarned.text = string.Format("{0}", currencyRound);
        StartCoroutine(CountCoinTotal(currencyRound,total));
    }
    IEnumerator CountCoinTotal(int current,float total)
    {
        int totalInt=Mathf.FloorToInt(total);
        yield return new WaitForSeconds(0.75f);
        coinsEarned.enabled = false;
        coinsAmountTotal.text = string.Format("Coins: {0}",totalInt);
        yield return null;
    }

    //health set
    public void SetHealth(float currentHealth,float maxHealth)
    {
        health.fillAmount = currentHealth / maxHealth;
        int temphealth = (int)currentHealth;
        healthNumber.text=temphealth.ToString();
    }

    //item coins flash red for can not be able to purchase
    public void TextFlash(TextMeshProUGUI text)
    {

    }

    public void UpdateResetCost(int cost)
    {
        reset.text = string.Format("{0}", cost);
    }
}
