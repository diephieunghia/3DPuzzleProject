using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IDamageable;
[RequireComponent(typeof(ArcherAction))]
public class ArcherBase : BaseChar
{
    ArcherBlackBoard bb;
    ArcherAction archer;
    protected override void Awake()
    {
        base.Awake();
        archer = GetComponent<ArcherAction>();
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

    protected override void IncreaseLevel(float value) { 
        Debug.Log($"Archer level increased by {value}");
    }
}
