using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : IState
{
    AttackStateHandler handler;
    Transform ownerTransform;
    ArcherBlackBoard atkbb;
    CharacterController controller;

    public Shoot(ArcherBlackBoard bb, Transform transform, CharacterController controller,AttackStateHandler atkHandler)
    {
        atkbb = bb;
        this.ownerTransform = transform;
        this.controller = controller;
        this.handler = atkHandler;
    }
    public void Enter() {       
    }

    public void Execute()
    {
        if (atkbb.aiming==ArcherBlackBoard.Aim.Cancel)
            handler.MoveChangeState(handler.idle);
        else if (atkbb.aiming == ArcherBlackBoard.Aim.Hold)
            return;
        //handle arrow projectile
        


        handler.MoveChangeState(handler.idle);
    }

    public void Exit() {
    }
}
