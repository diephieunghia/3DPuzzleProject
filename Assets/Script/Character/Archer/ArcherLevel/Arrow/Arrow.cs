using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
[RequireComponent(typeof(ArrowDetection))]
public class Arrow : MonoBehaviour
{
    [SerializeField] Rigidbody rb;

    ArrowDetection arrowDetection;

    //origin position
    Vector3 originPosition;
    Quaternion originRotate;
    public Vector3 OriginPosition { get; }
    public Quaternion OriginRotation { get; }
    public Transform arrowMass;
    //arrow damage
    float damage;
    public float Damage => damage;
    
    BoxCollider boxCollider;
    public BoxCollider BoxCollider { set
        {
            boxCollider.enabled = false;
        } }

    public TrailRenderer trail;

    //attribute
    bool fire=false;
    bool ice=false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = arrowMass.position;
        originPosition = transform.localPosition;
        originRotate = transform.localRotation;
        boxCollider = GetComponentInChildren<BoxCollider>();
        //boxCollider.enabled = false; // Disable collider initially
        trail=GetComponentInChildren<TrailRenderer>();
        trail.enabled = false;
        arrowDetection = GetComponent<ArrowDetection>();
    }
    private void Start()
    {
        
    }
    private void LateUpdate()
    {
        if(rb.velocity != Vector3.zero) {
            transform.rotation = Quaternion.LookRotation(-rb.velocity);
        }
    }

    public void ShootArrow(float force,float damage)
    {
        transform.parent = null;
        //boxCollider.enabled = true; // Enable collider when shooting
        rb.useGravity = true ;
        trail.enabled= true ;       
        this.damage = damage;
        rb.AddRelativeForce(-Vector3.forward * force, ForceMode.Impulse);
        if(arrowDetection.isActiveAndEnabled) 
            arrowDetection.coroutineStart?.Invoke();
    }
    public void SetFire()
    {
        fire = true;
    }
    public void SetIce() { ice = true; }
    

}
