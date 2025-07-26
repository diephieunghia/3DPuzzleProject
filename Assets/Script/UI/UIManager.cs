using System.Collections;
using System.Collections.Generic;
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


}
