using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStateHandler 
{
    public IState CurrentState;
    CharacterController controller;
    Transform ownerTransform;
    ArcherBlackBoard movementbb;

    public AttackStateHandler(ArcherBlackBoard bb, Transform transform, CharacterController controller)
    {
        movementbb = bb;
        ownerTransform = transform;
        this.controller = controller;
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
