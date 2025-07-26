using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    public Image[] images;

    public Action<float, int> coolDownAction;
    private void Awake()
    {
        coolDownAction= UpdateCoolDownUI;
    }
    public void UpdateCoolDownUI(float time,int index)
    {
        images[index].fillAmount = time;
    }

}
