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
    void Start()
    {
        arrowTip= GetComponentInChildren<BoxCollider>();
        body= GetComponentInChildren<Rigidbody>();
        arrow= GetComponentInChildren<Arrow>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == ally)
        {
            Debug.Log("Ally detected" + other.name);
            return;
        }
        body.velocity = Vector3.zero;
        body.useGravity = false;

        transform.position= Vector3.zero;
        transform.rotation = Quaternion.identity;

        Debug.Log(other.name);

        ArrowPool.ins.ReturnObject(gameObject);
    }

}
