using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public float rate = 3f;
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

        levelUI.fillAmount = value;
        levelText.text = string.Format("Level: {0}", level);
    }
    IEnumerator LevelUp(float value,float maxEP, int level)
    {
        //play animation

        yield return null;
    }
}
