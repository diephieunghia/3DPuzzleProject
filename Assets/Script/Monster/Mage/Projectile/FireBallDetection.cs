using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(ProjectileMove))]
public class FireBallDetection : MonoBehaviour
{
    [SerializeField] LayerMask playerMask;
    [SerializeField] LayerMask monsterMask;
    ProjectileMove firebalMove;
    ParticleSystem pSystem;
    float damage=0;
    public float Damage { set { damage = value; } }
    void Start()
    {
        firebalMove = GetComponent<ProjectileMove>();
        pSystem = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        
    }
    public void getDamage(float _damage)
    {
        damage = _damage;
    }

    private void OnTriggerEnter(Collider other)
    {      
        if (other.gameObject.CompareTag("Projectile")||other.gameObject.layer==monsterMask)
        {
            return;
        }
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collide player");
            IDamageable.Body type=IDamageable.Body.Body;
            IDamageable damageable = other.GetComponentInParent<IDamageable>();
            if (damageable != null)
            {
                Debug.Log("Apply damage to player: "+damage);
                damageable.TakeDamage(damage, transform.position, transform.forward, gameObject,type);
                //stop the fireball and return to pool
                firebalMove.Speed = 0;
                pSystem.Stop();
                transform.position = Vector3.zero;
                MageProjectilePool.ins.ReturnObject(gameObject);
            }
        }
        

    }
    
}
