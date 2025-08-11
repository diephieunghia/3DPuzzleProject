using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : ItemGetData
{
    public GameObject meteor;
    public SpriteRenderer circle;
    [SerializeField] SphereCollider sphereCollider;

    float tempCD;
    bool skillIsPlaying = true;
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
        if (tempCD <= 0&&skillIsPlaying)
        {
            skillIsPlaying = false;
        }
    }

   

}
