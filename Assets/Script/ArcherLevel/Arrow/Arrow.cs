using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Arrow : MonoBehaviour
{
    public Transform bowPosition;
    public Transform aimTarget;
    public Transform arrowTip;

    //test ray
    Ray ray;
    void Start()
    {       

    }

    void Update()
    {
        //follow bow
        transform.position = bowPosition.position;
        transform.rotation = bowPosition.rotation;
        //rotate follow aim target


        ray = new Ray(arrowTip.position, aimTarget.position - transform.position);
        Debug.DrawLine(ray.origin, (aimTarget.position - transform.position )* 10f,Color.green);


    }
}
