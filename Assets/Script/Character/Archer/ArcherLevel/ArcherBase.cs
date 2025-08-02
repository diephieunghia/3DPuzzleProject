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
}
