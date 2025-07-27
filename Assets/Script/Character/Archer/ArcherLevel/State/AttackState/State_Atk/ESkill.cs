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
        Debug.Log("Enter E skill");
        HandleAnim.ins.EAnim(true);
    }

    public void Execute()
    {
        tempDuration -= Time.deltaTime;
        if (tempDuration < 0) {
            atkbb.skill = false;
            atkbb.eSkill = false;
            atkbb.allowShoot = false;
            handler.MoveChangeState(handler.idle);
        }
        if(atkbb.aiming==ArcherBlackBoard.Aim.Shoot)
        {         
            HandleAnim.ins.EShoot();
            
            
            
        }
        //disable attack 
        if (HandleAnim.ins.AnimOverDrawEnd())
        {
            if(atkbb.allowShoot) 
                atkbb.currentArrow.GetComponent<Arrow>().ShootArrow(atkbb.maxForce, atkbb.maxDamage);
            Debug.Log("Disable attack anim");
            HandleAnim.ins.DisableAttack();
            atkbb.aiming = ArcherBlackBoard.Aim.Idle;
            atkbb.currentArrow = ArrowPool.ins.GetObject();
            atkbb.allowShoot=false;
        }
    }

    public void Exit() {
        HandleAnim.ins.EAnim(false);        
    }
}
