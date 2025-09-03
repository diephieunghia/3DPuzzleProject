using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESkill : IState
{
    AttackStateHandler handler;
    Transform ownerTransform;
    ArcherBlackBoard atkbb;
    CharacterController controller;

    float tempDuration;
    bool reloadOnce;

    public ESkill(ArcherBlackBoard bb, Transform transform, CharacterController controller, AttackStateHandler atkHandler)
    {
        atkbb = bb;
        this.ownerTransform = transform;
        this.controller = controller;
        handler = atkHandler;
    }
    public void Enter()
    {
        tempDuration = atkbb.eDuration;
        HandleAnim.ins.EAnim();
    }

    public void Execute()
    {
        Debug.Log("Eskill");
        tempDuration -= Time.deltaTime;
        if (tempDuration < 0&&(HandleAnim.ins.ReloadESkill(0.3f)||HandleAnim.ins.AnimOverDrawEnd(.3f))) {

            atkbb.skill = false;
            //atkbb.eSkill = false;
            handler.MoveChangeState(handler.idle);
        }
        
        if (atkbb.aiming==ArcherBlackBoard.Aim.Shoot&& HandleAnim.ins.ReloadESkill(.3f))
        {         
            HandleAnim.ins.EShoot();
        }
        if(atkbb.allowShoot)
        {
            for (int i = 0; i < atkbb.arrowCount; i++)
            {
                atkbb.currentArrow[i].GetComponent<Arrow>().ShootArrow(atkbb.maxForce, atkbb.maxDamage * .8f);
            }
            atkbb.allowShoot = false;
            reloadOnce = true;
            atkbb.aiming = ArcherBlackBoard.Aim.Idle;
            HandleAnim.ins.DisableEShoot();
        }
        if (HandleAnim.ins.ReloadESkill(.3f)&&reloadOnce)
        {
            float z = 0;
            for (int i = 0; i < atkbb.arrowCount; i++)
            {              
                atkbb.currentArrow[i] = ArrowPool.ins.GetObject();
                if (i == 1) z = -15;
                else if (i == 2) z = 15;
                atkbb.currentArrow[i].transform.localRotation = Quaternion.Euler(0, z, 0);
            }
            reloadOnce = false;
        }
    }

    public void Exit() {  
        atkbb.allowShoot=false;
        reloadOnce = false;
        
        HandleAnim.ins.DisableEShoot();
        for (int i = 0; i < atkbb.arrowCount; i++)
        {
            if (atkbb.currentArrow[i] != null)
            {
                ArrowPool.ins.ReturnObject(atkbb.currentArrow[i]);               
            }
            atkbb.currentArrow[i] = null;
        }
        
    }
}
