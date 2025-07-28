using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(ArcherAction))]
public class BaseChar : MonoBehaviour,IDamageable
{
    ArcherAction archer;

    void Awake()
    {
        archer = GetComponent<ArcherAction>();
    }

    //take damage
    public virtual void TakeDamage(float damage, Vector3 hitPoint, Vector3 hitDirection, GameObject attacker, IDamageable.Body hitPart)
    {

    }


    //increase stat


}
