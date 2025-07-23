using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Axe : MonoBehaviour
{
    [Header("Ray start end Position")]
    public Vector3 startPosition;
    public Vector3 endPosition;

    [SerializeField] GameObject rayStart;
    [SerializeField] GameObject rayEnd;
    Ray detectRay;
    public float distance = 2f;
    public LayerMask layerMask;

    BaseMonster baseMonster;
    // Start is called before the first frame update
    void Start()
    {
        baseMonster=GetComponentInParent<BaseMonster>();
    }

    // Update is called once per frame
    void Update()
    {       
        if(baseMonster.axeEnable)
        {
            DrawRay();
        }
              
    }

    void DrawRay()
    {    
        detectRay.direction = rayEnd.transform.position-rayStart.transform.position;
        Debug.DrawRay(rayStart.transform.position, detectRay.direction*distance, Color.red);
        if(Physics.Raycast(rayStart.transform.position, detectRay.direction, out RaycastHit hit, distance))
        {
            if(hit.collider.gameObject.GetComponent<IDamageable>() != null)
            {
                Debug.Log("Hit " + hit.collider.gameObject.name);
            }           
            
        }

    }

}
