using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(ProjectileMove))]
public class FireBallDetection : MonoBehaviour
{
    [SerializeField] LayerMask playerMask;
    [SerializeField] LayerMask monsterMask;
    [SerializeField] LayerMask projectile;
    ProjectileMove firebalMove;
    ParticleSystem pSystem;
    void Start()
    {
        firebalMove = GetComponent<ProjectileMove>();
        pSystem = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile")||other.gameObject.layer==monsterMask)
        {
            return;
        }
        if (other.gameObject.layer == playerMask)
        {
            IDamageable.Body type=IDamageable.Body.Body;
            IDamageable damageable = other.GetComponentInParent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(1, transform.position, transform.forward, gameObject,type);
            }
        }
        //stop the fireball and return to pool
        firebalMove.Speed = 0;
        pSystem.Stop();
        transform.position = Vector3.zero;
        MageProjectilePool.ins.ReturnObject(gameObject);

    }
    
}
