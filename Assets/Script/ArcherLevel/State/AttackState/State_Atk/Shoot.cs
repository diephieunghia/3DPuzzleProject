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
        if (atkbb.aiming == ArcherBlackBoard.Aim.Cancel)
        { 
            handler.MoveChangeState(handler.idle); 
        }
        else if (atkbb.aiming == ArcherBlackBoard.Aim.Hold)
        {
            
            atkbb.speed = Mathf.Lerp(atkbb.speed, 2, Time.deltaTime * 130f);                   
            return;
        }
            
            Debug.Log("Change to shoot");
            //handle arrow projectile
            atkbb.speed = atkbb.tempSpeed;


            handler.MoveChangeState(handler.idle);
        
    }

    public void Exit() {
    }


}
