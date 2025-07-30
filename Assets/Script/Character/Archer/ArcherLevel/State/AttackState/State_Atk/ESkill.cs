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
    bool attack=true;
    public ESkill(ArcherBlackBoard bb, Transform transform, CharacterController controller, AttackStateHandler atkHandler)
    {
        atkbb = bb;
        this.ownerTransform = transform;
        this.controller = controller;
        handler = atkHandler;
    }
    public void Enter()
    {
        attack = true;
        tempDuration = atkbb.eDuration;
        Debug.Log("Enter E skill");
        HandleAnim.ins.EAnim();
    }

    public void Execute()
    {

        tempDuration -= Time.deltaTime;
        if (tempDuration < 0) {
            atkbb.skill = false;
            atkbb.eSkill = false;
            //check if animation is finished
            if (HandleAnim.ins.AnimOverDrawEnd())
                handler.MoveChangeState(handler.idle);
        }
        
        if (atkbb.aiming==ArcherBlackBoard.Aim.Shoot&&attack)
        {         
            HandleAnim.ins.EShoot();                        
        }
        //disable attack 
        if (HandleAnim.ins.AnimOverDrawEnd()&&attack)
        {
            for (int i = 0; i < atkbb.arrowCount; i++)                         
               atkbb.currentArrow[i].GetComponent<Arrow>().ShootArrow(atkbb.maxForce, atkbb.maxDamage * .8f);                           
            HandleAnim.ins.DisableESkill();
            atkbb.aiming = ArcherBlackBoard.Aim.Idle;
            reloadOnce = true;
            attack= false;
        }
        if (HandleAnim.ins.WaitForReloadAnim() && reloadOnce)
        {
            float z = 0;
            for (int i = 0; i < atkbb.arrowCount; i++)
            {
                if (atkbb.currentArrow[i] == null)
                    atkbb.currentArrow[i] = ArrowPool.ins.GetObject();
                if (i == 1) z = -15;
                else if (i == 2) z = 15;
                atkbb.currentArrow[i].transform.localRotation = Quaternion.Euler(0, z, 0);
            }
            reloadOnce = false;
            attack = true;
        }
    }

    public void Exit() {  
        attack = false;
        reloadOnce = false;       
        HandleAnim.ins.DisableESkill();
        Debug.Log("Change to Idle");
        for (int i = 0; i < atkbb.arrowCount; i++)
        {
            if (atkbb.currentArrow[i] != null)
            {
                Debug.Log("return arrow");
                ArrowPool.ins.ReturnObject(atkbb.currentArrow[i]);               
            }
            atkbb.currentArrow[i] = null;
        }
    }
}
