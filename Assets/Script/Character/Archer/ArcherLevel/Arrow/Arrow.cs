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
    public Transform arrowMass;

    BoxCollider boxCollider;
    public BoxCollider BoxCollider { set
        {
            boxCollider.enabled = false;
        } }

    public TrailRenderer trail;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = arrowMass.position;
        originPosition = transform.localPosition;
        originRotate = transform.localRotation;
        boxCollider = GetComponentInChildren<BoxCollider>();
        boxCollider.enabled = false; // Disable collider initially
        trail=GetComponentInChildren<TrailRenderer>();
        trail.enabled = false;
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
        trail.enabled= true ;
        boxCollider.enabled = true; // Enable collider when shooting
        rb.AddRelativeForce(-Vector3.forward * force, ForceMode.Impulse);
    }

    
}
