using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGetData : MonoBehaviour
{
    protected float initialCD = 3f;
    protected float initialDamage = 50f;
    protected ArcherBlackBoard archerBlackBoard;
    protected virtual void Start()
    {
        GameManager.ins.GetCharStat += ApplyCharStat;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected virtual void ApplyCharStat(ArcherBlackBoard bb)
    {
        initialCD *= initialCD * bb.passiveCD;
        initialDamage *= initialDamage * bb.passiveDmg;
    }
    public void GetCharStatAtStart(ArcherBlackBoard bb)
    {
        archerBlackBoard = bb;
    }

}
