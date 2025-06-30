using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDetection : MonoBehaviour
{
    BoxCollider arrowTip;
    Rigidbody body;
    void Start()
    {
        arrowTip= GetComponentInChildren<BoxCollider>();
        body= GetComponentInChildren<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger enter");
        body.velocity = Vector3.zero;
        body.useGravity = false;
    }

}
