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
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        DrawRay();
    }

    void DrawRay()
    {
        detectRay.direction = rayStart.transform.position - rayEnd.transform.position;
        Debug.DrawLine(rayStart.transform.position, detectRay.direction, Color.green);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(rayStart.transform.position, rayEnd.transform.position);
    }
}
