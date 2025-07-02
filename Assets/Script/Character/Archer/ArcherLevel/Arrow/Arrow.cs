using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Arrow : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    //test ray
    Vector3 direction;
    Ray ray;

    //origin position
    Vector3 originPosition;
    Quaternion originRotate;
    public Vector3 OriginPosition { get; }
    public Quaternion OriginRotation { get; }
    //max draw back distance
    float maxDistance = 5f;
    public Transform arrowMass;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = arrowMass.position;
        originPosition = transform.localPosition;
        originRotate = transform.localRotation;
    }
    private void LateUpdate()
    {
        if(rb.velocity != Vector3.zero) {
            transform.rotation = Quaternion.LookRotation(-rb.velocity);
        }
    }

    public void ShootArrow(float force)
    {
        transform.parent = null;
        rb.useGravity = true ;
        rb.AddRelativeForce(-Vector3.forward * force, ForceMode.Impulse);
    }
    
}
