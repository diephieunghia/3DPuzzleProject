using Cinemachine.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Meteor : ItemGetData
{
    public GameObject meteor;
    public GameObject circle;
    public Transform startPos;
    public Transform destination;

    float tempCD;
    bool skillIsPlaying = true;

    public LayerMask enemy;
    //meteor ball
    float elapsedTime = 0f;
    float timeToReach = 1f;
    //circle sprite renderer
    float circleElapsed = 0f;
    float timeToFullCircle = 0.5f;

    //overlap sphere
    public float radius = 5.8f;

    protected override void Start()
    {
        base.Start();
           tempCD=initialCD;
      
    }

    // Update is called once per frame
    void Update()
    {
        tempCD-=Time.deltaTime;
        //play skill
        if (tempCD <= 0)
        {
            if (skillIsPlaying)
            {
                skillIsPlaying = false;
                //enable meteor ball
                meteor.SetActive(true);
            }
            //expand circle
            circleElapsed += Time.deltaTime;
            float t2 = Mathf.Clamp01(circleElapsed / timeToFullCircle);
            circle.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, t2);
           
            //start meteor
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / timeToReach);
            if (t == 1)
            {
                //Cast overlap sphere 
                Collider[] hit=Physics.OverlapSphere(destination.position,radius,enemy);
                foreach(Collider c in hit)
                {
                    IDamageable damageable=c.GetComponentInParent<IDamageable>();
                    if (damageable != null)
                    {
                        damageable.TakeDamage(initialDamage, transform.position, transform.forward, gameObject, IDamageable.Body.Body);
                    }
                }
                Debug.Log("Reach Destination"); 
                //disable ball
                meteor.SetActive(false);
                //set skill to true to continue playing
                skillIsPlaying = true;
                //set circle sprite to zero and circleElapsed
                circle.transform.localScale = Vector3.zero;
                circleElapsed = 0;
                //set ball elapsed time to 0
                elapsedTime = 0f;
                tempCD = initialCD;

            }
            meteor.transform.position = Vector3.Lerp(startPos.transform.position, destination.position, t);
            meteor.transform.LookAt(destination.position);

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);

    }




}
