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
    Ray ray;
    Vector3 direction;

    void Start()
    {
        rb= GetComponent<Rigidbody>();
        //StartCoroutine("Detach");
    }

    void Update()
    {
        //transform.rotation = Quaternion.Lerp(transform.rotation, bowPosition.rotation, 2f);          
        direction = transform.position - aimTarget.position;
        transform.rotation = Quaternion.LookRotation(direction);
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
