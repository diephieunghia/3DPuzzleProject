using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageProjectilePool : ObjectPool
{
    public static MageProjectilePool ins { get; private set; }

    private void Awake()
    {
        if (ins != null && ins != this)
            Destroy(this);
        else
            ins = this;
    }


}
