using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SoundBrightSen : MonoBehaviour
{
    public static SoundBrightSen ins;

    public float sound;
    public float bright;
    public float brightSaved;
    public float sen;

    private void Awake()
    {
        if (ins != null && ins != this)
            Destroy(this);
        else
            ins = this;
    }

}
