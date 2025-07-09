using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseChar : MonoBehaviour,IDamageable
{
    public virtual bool IsShooting()
    {
        return false;
    }

    public virtual void TakeDamage(float damage, Vector3 hitPoint, Vector3 hitDirection, GameObject attacker)
    {
    }


}
