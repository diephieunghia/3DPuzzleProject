using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimToTarget : MonoBehaviour
{
    [SerializeField] Transform target;
    Ray ray;
    Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Update()
    {
        ray = new Ray(transform.position, target.position - transform.position);
        Debug.DrawLine(transform.position, target.transform.position, Color.green);
    }

}
