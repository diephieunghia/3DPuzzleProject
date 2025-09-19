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
    public ESkill eSkill;

    public AttackStateHandler(ArcherBlackBoard bb, Transform transform, CharacterController controller)
    {
        movementbb = bb;
        ownerTransform = transform;
        this.controller = controller;
        idle= new ATKIdle(bb,transform, controller,this);
        shoot=new Shoot(bb,transform,controller,this);
        eSkill=new ESkill(bb,transform,controller,this);
        CurrentState = idle;
        CurrentState.Enter();
    }
    public void MoveChangeState(IState nextState)
    {
        CurrentState.Exit();
        CurrentState = nextState;
        CurrentState.Enter();
    }
    public void HandleAttackState()
    {
        if(!GameSettings.ins.isGamePaused) 
            CurrentState.Execute();
    }

}
