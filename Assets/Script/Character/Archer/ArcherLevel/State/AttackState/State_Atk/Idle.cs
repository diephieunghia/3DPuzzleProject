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
        float z = 0;
        for (int i = 0; i < atkbb.arrowCount; i++) {
            Debug.Log("Arrow create ");
            atkbb.currentArrow[i] = ArrowPool.ins.GetObject();
            if (i == 1) z = -15;
            else if (i == 2) z = 15;
            atkbb.currentArrow[i].transform.localRotation = Quaternion.Euler(0, z, 0);
        }       
    }

    public void Execute() {
        Debug.Log("Idle");
        
        if (HandleAnim.ins.ReloadESkill(.8f))
        {
            HandleAnim.ins.DisableEShoot();    
        }
        if (atkbb.eSkill&&atkbb.skill)
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
