using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStateHandler 
{
    public IState CurrentState;

    CharacterController controller;
    Transform ownerTransform;
    ArcherBlackBoard movementbb;

    public ATKIdle idle;
    public Shoot shoot;

    public AttackStateHandler(ArcherBlackBoard bb, Transform transform, CharacterController controller)
    {
        movementbb = bb;
        ownerTransform = transform;
        this.controller = controller;
        idle= new ATKIdle(bb,transform, controller,this);
        shoot=new Shoot(bb,transform,controller,this);
        CurrentState = idle;
    }
    public void MoveChangeState(IState nextState)
    {
        CurrentState.Exit();
        CurrentState = nextState;
        CurrentState.Enter();
    }
    public void HandleAttackState()
    {
        CurrentState.Execute();
    }

}
