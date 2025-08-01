using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(ArcherAction))]
public class SkillHandler : MonoBehaviour
{
    //Description: Skill handler calculate skill cooldown and enable skills, use UI manager to show on screen
    ArcherBlackBoard skillStat;

    public Action<float,int> skillDelegate;

    // Start is called before the first frame update
    void Start()
    {
        skillStat = GetComponent<ArcherAction>().bb;     
        skillDelegate = CDMethod;
    }

    void CDMethod(float coolDownTime,int index)
    {
        float[] Time2=new float[] {coolDownTime,coolDownTime};
        StartCoroutine(CountDown(Time2,index));
    }
    IEnumerator CountDown(float[] time,int index)
    {
        while (time[0] > 0)
        {
            time[0] -= Time.deltaTime;
            UIManager.ins.skillManager.coolDownAction.Invoke((time[1] - time[0]) / time[1],index);
            yield return null;
        }
        EnableSkill(index);
        yield break;
    }
    void EnableSkill(int index)
    {
        if (index == 0)
            skillStat.dash = true;

    }
}
