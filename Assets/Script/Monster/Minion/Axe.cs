using System.Collections;
using System.Collections.Generic;
using System.Net;
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
        Debug.Log("Triggered with " + other.gameObject.name);
    }

    void DrawRay()
    {    
        detectRay.direction = rayEnd.transform.position-rayStart.transform.position;
        Debug.DrawRay(rayStart.transform.position, detectRay.direction*distance, Color.red);
        if(Physics.Raycast(rayStart.transform.position, detectRay.direction, out RaycastHit hit, distance,layerMask))
        {
            if(hit.collider.gameObject.GetComponent<IDamageable>() != null)
            {
                hit.collider.gameObject.GetComponent<IDamageable>().TakeDamage(baseMonster.MonsterStat.damage,hit.transform.position,detectRay.direction,gameObject,IDamageable.Body.Body);
                Debug.Log("Hit " + hit.collider.gameObject.name);
            }           
            
        }

    }

}
