using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : IState
{
    AttackStateHandler handler;
    Transform ownerTransform;
    ArcherBlackBoard atkbb;
    CharacterController controller;

    float elapsedTime = 0;
    float timeFullCharge = 2.5f;


    public Shoot(ArcherBlackBoard bb, Transform transform, CharacterController controller,AttackStateHandler atkHandler)
    {
        atkbb = bb;
        this.ownerTransform = transform;
        this.controller = controller;
        this.handler = atkHandler;
    }
    public void Enter() {
        HandleAnim.ins.SetAttackAnim();
    }

    public void Execute()
    {
        atkbb.shootRate += Time.deltaTime;
        if (atkbb.aiming == ArcherBlackBoard.Aim.Cancel)
        {
            handler.MoveChangeState(handler.idle);
            atkbb.shootRate = 0f;
        }
        else if (atkbb.aiming == ArcherBlackBoard.Aim.Hold)
        {
            elapsedTime += Time.deltaTime;
            float completion = elapsedTime / timeFullCharge;

            atkbb.speed = Mathf.Lerp(atkbb.speed, 2, completion); 
            atkbb.force=Mathf.Lerp(10,atkbb.maxForce, completion);           
            return;
        }
        else
        { elapsedTime = 0; }
        if (atkbb.allowShoot)
        {
            atkbb.currentArrow.GetComponent<Arrow>().ShootArrow(atkbb.force);
            atkbb.aiming = ArcherBlackBoard.Aim.Idle;
            handler.MoveChangeState(handler.idle);
            atkbb.allowShoot = false;
        }
               
    }

    public void Exit() {
        HandleAnim.ins.DisableAttack();
        atkbb.speed = atkbb.tempSpeed;
        atkbb.force = 0.2f;
        atkbb.shootRate = 0f;
        elapsedTime = 0;
        atkbb.currentArrow = null;
    }


}
