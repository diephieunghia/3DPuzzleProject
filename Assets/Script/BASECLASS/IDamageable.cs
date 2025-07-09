using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void TakeDamage(float damage, Vector3 hitPoint, Vector3 hitDirection, GameObject attacker);
}
