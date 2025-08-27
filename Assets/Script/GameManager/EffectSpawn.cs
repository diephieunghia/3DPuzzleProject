using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
public enum EffectName
{
    Normal,
    Head,
    Fire,
    Ice
}

public class EffectSpawn : MonoBehaviour
{
    public static EffectSpawn ins;
    public ObjectPool[] vfx;
    
    private void Awake()
    {
        if (ins != null && ins != this)
            Destroy(this);
        else
            ins = this;
    }
    private void Start()
    {
        vfx = GetComponentsInChildren<ObjectPool>();
    }
    public GameObject GetEffect(EffectName name)
    {
        int index = (int)name;
        return vfx[index].GetObject();
    }
    


}
