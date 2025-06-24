using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Arrow : MonoBehaviour
{
    public Transform bowPosition;
    public Transform aimTarget;
    [SerializeField] Rigidbody rb;

    //test ray
    Vector3 direction;
    Ray ray;

    //max draw back distance
    float maxDistance = 5f;


    void Start()
    {
        rb= GetComponent<Rigidbody>();
        //StartCoroutine("Detach");
    }

    void Update()
    {
        ArrowIdle();
    }

    void ArrowIdle()
    {
        direction = transform.position - aimTarget.position;
        transform.rotation = Quaternion.LookRotation(direction);

        ray = new Ray(transform.position, aimTarget.position - transform.position);
        Debug.DrawLine(transform.position, aimTarget.transform.position, Color.green);
    }
    void ArrowDrawBack()
    {
        
    }
    void ShootArrow()
    {

    }
    IEnumerator Detach()
    {
        yield return new WaitForSeconds(2f);
        transform.parent = null;
        rb.AddRelativeForce(-Vector3.forward*8f,ForceMode.Impulse);

    }
}
