using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Axe : MonoBehaviour
{
    [Header("Ray start end Position")]
    public Vector3 startPosition;
    public Vector3 endPosition;

    [SerializeField] GameObject rayStart;
    [SerializeField] GameObject rayEnd;
    Ray detectRay;
    public float distance = 2f;
    public LayerMask layerMask;

    BaseMonster baseMonster;
    // Start is called before the first frame update
    void Start()
    {
        baseMonster=GetComponentInParent<BaseMonster>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (baseMonster.Attack)
        {
            IDamageable playerTakeDamage = other.GetComponent<IDamageable>();
            if (playerTakeDamage != null)
            {
                SoundManager.ins.PlaySoundOneShot(SoundType.AxeHit, 1);
                playerTakeDamage.TakeDamage(baseMonster.MonsterStat.damage, transform.position, transform.forward, gameObject, IDamageable.Body.Body);
            }
        }
    }

   

}
