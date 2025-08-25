using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public enum Body
    {
        Head,
        Body,
    }
    void TakeDamage(float damage, Vector3 hitPoint, Vector3 hitDirection, GameObject attacker,Body type);

    void FireDamage(float damage, Vector3 hitPoint, Vector3 hitDirection, GameObject attacker, Body type);

    void IceDamage(float damage, Vector3 hitPoint, Vector3 hitDirection, GameObject attacker, Body type);
}
