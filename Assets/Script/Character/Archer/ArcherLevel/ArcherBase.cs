using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IDamageable;
[RequireComponent(typeof(ArcherAction))]
public class ArcherBase : BaseChar
{
    ArcherBlackBoard bb;
    ArcherAction archer;

    //level handle
    float currentEXP=0;
    protected override void Awake()
    {
        base.Awake();
        archer = GetComponent<ArcherAction>();
        GameManager.ins.LevelChange += IncreaseLevel;
    }
    private void Start()
    {
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
            //max exp will increase here
            UIManager.ins.LevelChange(bb.maxEXP, bb.maxEXP, bb.level);
        }
        UIManager.ins.LevelChange(currentEXP, bb.maxEXP, bb.level);
    }
}
