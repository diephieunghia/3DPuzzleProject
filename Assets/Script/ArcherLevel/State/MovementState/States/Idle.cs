using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : IState
{
    Transform ownerTransform;
    ArcherBlackBoard movementbb;
    MovementStateHandler statehandler;
    CharacterController controller;

    public Idle(Transform ownerTransform, ArcherBlackBoard movementbb,MovementStateHandler handler, CharacterController characterController)
    {
        this.ownerTransform = ownerTransform;
        this.movementbb = movementbb;
        this.statehandler = handler;
        controller = characterController;
    }

    public void Enter()
    {
        
    }

    public void Execute()
    {
        if (movementbb.velocity.magnitude > 0.01f)
        {
            statehandler.MoveChangeState(statehandler.move);
        }
        if(!movementbb.isGround) {
            statehandler.MoveChangeState(statehandler.jump);
        }

    }

    public void Exit()
    {

    }
}


