using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic : IState
{
    
    Transform ownerTransform;
    ArcherBlackBoard atkbb;   
    CharacterController controller;

    public Basic(ArcherBlackBoard bb, Transform transform, CharacterController controller)
    {
        atkbb = bb;
        ownerTransform = transform;
        this.controller=controller;
    }
    public void Enter() { }

    public void Execute() { 

    }

    public void Exit() { }
}
