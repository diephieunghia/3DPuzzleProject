using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using UnityEngine;
[RequireComponent(typeof(Arrow))]
public class ArrowDetection : MonoBehaviour
{
    Arrow arrow;

    BoxCollider arrowTip;
    Rigidbody body;
    public LayerMask ally;

    //return to pool after set of time
    float returnTime = 10f;
    bool coroutineFinished = false;

    public Action coroutineStart;
    private void Awake()
    {
        arrowTip = GetComponentInChildren<BoxCollider>();
        body = GetComponentInChildren<Rigidbody>();
        arrow = GetComponentInChildren<Arrow>();
    }
    void Start()
    {
            coroutineStart+= TriggerCoroutine;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == ally||other.gameObject.CompareTag("Projectile"))
        {
            return;
        }
        if(gameObject.transform.parent != null)
        {
            // If the arrow is still parented to the archer, ignore the hit
            return;
        }
        //Debug.Log("Arrow hit: " + other.name);
        IDamageable damageable = other.GetComponentInParent<IDamageable>();
        //get contact point
        Vector3 closetPoint=other.ClosestPointOnBounds(other.transform.position);
        if (damageable != null)
        {
            IDamageable.Body type=IDamageable.Body.Body;
            float multiplier = 1f;
            if (other.CompareTag("Head"))
            { 
                multiplier = 2f;
                type = IDamageable.Body.Head;
            }       
            damageable.TakeDamage(arrow.Damage * multiplier, closetPoint, transform.forward, gameObject,type);
        }
        if(!coroutineFinished)
            StopCoroutine(ReturnToPool());
        body.velocity = Vector3.zero;
        body.useGravity = false;

        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        //arrow.BoxCollider = null;

        arrow.trail.enabled = false;
        ArrowPool.ins.ReturnObject(gameObject);
    }
    IEnumerator ReturnToPool()
    {
        yield return new WaitForSeconds(returnTime);
        body.velocity = Vector3.zero;
        body.useGravity = false;

        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        //arrow.BoxCollider = null;

        arrow.trail.Clear();
        arrow.trail.enabled = false;
        ArrowPool.ins.ReturnObject(gameObject);

        coroutineFinished = true;
    }
    void TriggerCoroutine()
    {
        StartCoroutine(ReturnToPool());
    }
}
