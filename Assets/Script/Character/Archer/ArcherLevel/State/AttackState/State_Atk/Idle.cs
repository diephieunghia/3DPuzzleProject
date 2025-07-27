using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATKIdle : IState
{
    AttackStateHandler handler;
    Transform ownerTransform;
    ArcherBlackBoard atkbb;   
    CharacterController controller;

    public ATKIdle(ArcherBlackBoard bb, Transform transform, CharacterController controller,AttackStateHandler atkHandler)
    {
        atkbb = bb;
        this.ownerTransform = transform;
        this.controller = controller;
        handler = atkHandler;
    }
    public void Enter() {
        atkbb.currentArrow = ArrowPool.ins.GetObject();
    }

    public void Execute() {
        if(atkbb.eSkill)
        {
            // change to E skill state
            handler.MoveChangeState(handler.eSkill);
        }
        if(HandleAnim.ins.WaitForReloadAnim())
            if (atkbb.aiming==ArcherBlackBoard.Aim.Hold)
                handler.MoveChangeState(handler.shoot);
    }

    public void Exit() { }
}
