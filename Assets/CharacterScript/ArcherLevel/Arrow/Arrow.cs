using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Arrow : MonoBehaviour
{
    [SerializeField] Rigidbody rb;

    //test ray
    Vector3 direction;
    Ray ray;

    //max draw back distance
    float maxDistance = 5f;

    //arrow state


    void Start()
    {
        rb= GetComponent<Rigidbody>();
    }

    void Update()
    {
    }

    public void ShootArrow(float force)
    {
        transform.parent = null;
        rb.useGravity = true ;
        rb.AddRelativeForce(-Vector3.forward * force, ForceMode.Impulse);
    }
    IEnumerator Detach()
    {
        yield return new WaitForSeconds(2f);
        transform.parent = null;
        rb.AddRelativeForce(-Vector3.forward*8f,ForceMode.Impulse);

    }
}
