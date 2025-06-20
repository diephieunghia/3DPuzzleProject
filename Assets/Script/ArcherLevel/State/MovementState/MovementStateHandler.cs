using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public enum MovementState
{
    Idle,
    Walk,
    Jump,
    DoubleJump,
    Fall,
    Dash
}
public class MovementStateHandler 
{
    public IState CurrentState;
    CharacterController controller;
    Transform ownerTransform;
    ArcherBlackBoard movementbb;

    public Idle idle;
    public Move move;
    public Jump jump;

    public MovementStateHandler(ArcherBlackBoard bb,Transform transform, CharacterController controller)
    {
        movementbb = bb;
        ownerTransform = transform;
        idle = new Idle(ownerTransform, movementbb, this,controller);
        move = new Move(ownerTransform, movementbb, this,controller);
        jump=new Jump(ownerTransform, movementbb, this, controller);
        this.controller = controller;
        CurrentState = idle;
    }
    public void MoveChangeState(IState nextState)
    {
        CurrentState.Exit();
        CurrentState = nextState;
        CurrentState.Enter();
    }
    public void HandleMovementState()
    {
        CurrentState.Execute();
    }

}
