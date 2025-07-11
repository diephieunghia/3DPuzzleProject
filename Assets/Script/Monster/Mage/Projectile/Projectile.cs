using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float radius = 0.5f;
    public LayerMask playerMask;
    Collider[] hitColliders=null;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void SphereCast()
    {
        hitColliders = Physics.OverlapSphere(transform.position, radius, playerMask);
        if (hitColliders != null)
        {
            foreach (Collider hitCollider in hitColliders)
            {
                if (hitCollider.gameObject.GetComponent<IDamageable>() != null)
                    Debug.Log("here to deliver damage to player " + hitCollider.gameObject.name);
            }
            hitColliders = null;
        }
    }

}
