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
            atkbb.allowShoot = false;
            handler.MoveChangeState(handler.idle);
            atkbb.shootRate = 0f;
        }
        else if (atkbb.aiming == ArcherBlackBoard.Aim.Hold)
        {
            elapsedTime += Time.deltaTime;
            float completion = elapsedTime / timeFullCharge;

            atkbb.speed = Mathf.Lerp(atkbb.speed, 2, completion);            
            atkbb.force=Mathf.Lerp(8,atkbb.maxForce, completion);
            atkbb.damage = Mathf.Lerp(20, atkbb.maxDamage, completion);
            return;
        }
        else
        { elapsedTime = 0; }
        if (atkbb.allowShoot)
        {
            for (int i = 0; i < atkbb.arrowCount; i++)
            {
                atkbb.currentArrow[i].GetComponent<Arrow>().ShootArrow(atkbb.force, atkbb.damage);
            }
            atkbb.aiming = ArcherBlackBoard.Aim.Idle;
            handler.MoveChangeState(handler.idle);
            atkbb.allowShoot = false;
           
        }
               
    }

    public void Exit() {
        HandleAnim.ins.DisableAttack();
        atkbb.speed = atkbb.tempSpeed;
        atkbb.force = 8f;
        atkbb.shootRate = 0f;
        elapsedTime = 0;
        for (int i = 0; i < atkbb.arrowCount; i++)               
            atkbb.currentArrow[i] = null;       
    }


}
