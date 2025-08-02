using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
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
    [Header("Level")]
    public Image levelUI;
    public TextMeshProUGUI levelText;
    public float rate = 10f;
    float lastValue = 0;
    int runCount = 0;
    private void Awake()
    {
        if (ins != null && ins != this)
            Destroy(this);
        else
            ins = this;
                
    }
    private void Start()
    {
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
            Debug.Log("Value: " + value + " maxEXP:" + maxEXP);
            Debug.Log(runCount);
            StartCoroutine(LevelUp(value, maxEXP, level));
        }
        //levelUI.fillAmount = value;
        //levelText.text = string.Format("Level: {0}", level);
            
        
        
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
}
