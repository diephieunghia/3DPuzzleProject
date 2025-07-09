using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Arrow))]
public class ArrowDetection : MonoBehaviour
{
    Arrow arrow;

    BoxCollider arrowTip;
    Rigidbody body;
    public LayerMask ally;
    private void Awake()
    {
        arrowTip = GetComponentInChildren<BoxCollider>();
        body = GetComponentInChildren<Rigidbody>();
        arrow = GetComponentInChildren<Arrow>();
    }
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == ally)
        {
            return;
        }
        body.velocity = Vector3.zero;
        body.useGravity = false;

        transform.position= Vector3.zero;
        transform.rotation = Quaternion.identity;
        arrow.BoxCollider = null;

        ArrowPool.ins.ReturnObject(gameObject);
    }

}
