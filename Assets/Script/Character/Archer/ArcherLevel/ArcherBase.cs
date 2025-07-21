using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IDamageable;
[RequireComponent(typeof(ArcherAction))]
public class ArcherBase : BaseChar
{
    ArcherBlackBoard bb;

    private void Start()
    {
        bb = GetComponent<ArcherAction>().bb;
    }
    public override bool IsShooting()
    {
        return bb.aiming == ArcherBlackBoard.Aim.Shoot;
    }

    public override void TakeDamage(float damage, Vector3 hitPoint, Vector3 hitDirection, GameObject attacker, IDamageable.Body hitPart)
    {
        // Implement damage logic here
        // For example, reduce health, play animation, etc.
        Debug.Log($"Archer took {damage} damage from {attacker.name} at {hitPoint}");
    }
}
