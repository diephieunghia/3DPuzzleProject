using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(ArcherAction))]
public class BaseChar : MonoBehaviour,IDamageable
{
    protected virtual void Awake()
    {
        
    }
    //take damage
    public virtual void TakeDamage(float damage, Vector3 hitPoint, Vector3 hitDirection, GameObject attacker, IDamageable.Body hitPart)
    { }

    //level
    protected virtual void IncreaseLevel(float value,float coins) {

    }

    public void DamgeType(bool fire, bool ice, float damage)
    {
        throw new System.NotImplementedException();
    }
}
