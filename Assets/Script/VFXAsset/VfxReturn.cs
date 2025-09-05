using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VfxReturn : MonoBehaviour
{
    ParticleSystem particle;
    ObjectPool parentPool;

    void Awake()
    {
        particle = GetComponent<ParticleSystem>();
        parentPool = GetComponentInParent<ObjectPool>();
    }
    // Start is called before the first frame update
   
    private void OnParticleSystemStopped()
    {
       
        parentPool.ReturnObject(gameObject);
    }

    public void Manualreturn()
    {
        particle.Stop(true,ParticleSystemStopBehavior.StopEmittingAndClear);
        parentPool.ReturnObject(gameObject);
    }

}
