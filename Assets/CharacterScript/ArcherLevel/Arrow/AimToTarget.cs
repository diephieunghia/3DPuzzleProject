using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimToTarget : MonoBehaviour
{
    [SerializeField] Transform aimTarget;
    Ray ray;
    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Update()
    {
        direction = transform.position - aimTarget.position;
        transform.rotation = Quaternion.LookRotation(direction);

        ray = new Ray(transform.position, aimTarget.position - transform.position);
        Debug.DrawLine(transform.position, aimTarget.transform.position, Color.green);
    }

}
