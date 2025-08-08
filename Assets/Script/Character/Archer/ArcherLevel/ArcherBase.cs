using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IDamageable;
[RequireComponent(typeof(ArcherAction))]
public class ArcherBase : BaseChar
{
    ArcherBlackBoard bb;
    ArcherAction archer;

    float currentEXP = 0;
    protected override void Awake()
    {
        base.Awake();
        archer = GetComponent<ArcherAction>();
        
    }
    private void Start()
    {
        GameManager.ins.LevelChange += IncreaseLevel;
        bb = GetComponent<ArcherAction>().bb;
        
    }
    //damage 
    public override void TakeDamage(float damage, Vector3 hitPoint, Vector3 hitDirection, GameObject attacker, IDamageable.Body hitPart)
    {
        // Implement damage logic here
        // For example, reduce health, play animation, etc.
        Debug.Log($"Archer took {damage} damage from {attacker.name} at {hitPoint}");
    }
    //Level Up
    protected override void IncreaseLevel(float value)  
    {
        currentEXP += value;
        while (currentEXP >= bb.maxEXP) { 
            currentEXP -= bb.maxEXP;
            bb.level += 1;
            UIManager.ins.LevelChange(bb.maxEXP, bb.maxEXP, bb.level);           
            //max exp will increase here         
            bb.maxEXP += 10f;
            //send message to increase stat
        }
        UIManager.ins.LevelChange(currentEXP, bb.maxEXP, bb.level);
    }
    //StatUp
    public void StatUP(SO_Item item)
    {
        bb.damage*=item.damage;
        bb.maxDamage*=item.maxDamage;
        bb.arrowCount=Mathf.Max(bb.arrowCount,item.arrowCount);
        bb.speed*=item.speed;
        bb.tempSpeed*=item.tempSpeed;
    }
    public void Special(SO_Item item)
    {
        if(bb.arrowCount<item.arrowCount)
        {
            bb.arrowCount=item.arrowCount;
            float z = 0;
            //disable
            for (int i = 0; i < bb.arrowCount; i++)
            {
                if (bb.currentArrow[i] != null)
                {
                    ArrowPool.ins.ReturnObject(bb.currentArrow[i]);
                }
                if (i == 1) z = -15;
                else if (i == 2) z = 15;
                bb.currentArrow[i] = ArrowPool.ins.GetObject();
                bb.currentArrow[i].transform.localRotation= Quaternion.Euler(0, z, 0);
            }
        }
    }
}
