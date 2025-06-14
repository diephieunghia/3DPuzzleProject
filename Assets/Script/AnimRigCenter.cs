using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimRigCenter : MonoBehaviour
{
    [SerializeField] Transform sourceObject;
    public float distance = 100f;
    [SerializeField] Camera cam;

    float minDistance = 10f;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //Ray ray=cam.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f));

        
        //RaycastHit hitSurface;
        //if (Physics.Raycast(ray, out hitSurface))
        //{
        //    Vector3 hitPosition = hitSurface.point;
        //    sourceObject.position = hitPosition;
        //    return;
        //}
        //sourceObject.position = ray.GetPoint(minDistance); 

        //Debug.DrawRay(ray.origin, ray.direction * distance, Color.yellow);

    }
   
}
