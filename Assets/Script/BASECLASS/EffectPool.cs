using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPool : ObjectPool
{
    public static EffectPool ins;
    private void Awake()
    {
        if (ins != null && ins != this)
            Destroy(this);
        else
            ins = this;
    }
      
}
