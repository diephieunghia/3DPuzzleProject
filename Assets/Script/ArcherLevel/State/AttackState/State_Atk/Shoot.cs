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
    float timeFullCharge = 2f;
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
            elapsedTime += Time.deltaTime;
            float completion = elapsedTime / timeFullCharge;

            atkbb.speed = Mathf.Lerp(atkbb.speed, 2, completion);
            Debug.Log(atkbb.speed);
            return;
        }
        atkbb.speed = atkbb.tempSpeed;

        handler.MoveChangeState(handler.idle);
        
    }

    public void Exit() {
    }


}
