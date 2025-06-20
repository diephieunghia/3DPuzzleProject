using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : IState
{
    Transform ownerTransform;
    ArcherBlackBoard movementbb;
    MovementStateHandler statehandler;
    CharacterController controller;

    public Move(Transform ownerTransform, ArcherBlackBoard movementbb, MovementStateHandler handler, CharacterController controller)
    {
        this.ownerTransform = ownerTransform;
        this.movementbb = movementbb;
        this.statehandler = handler;
        this.controller = controller;
    }
    public void Enter() { }

    public void Execute()
    {
        if (movementbb.velocity.magnitude < 0.01f)
            statehandler.MoveChangeState(statehandler.idle);
        if(!movementbb.isGround)
        {
            statehandler.MoveChangeState(statehandler.jump);
        }
        movementbb.Horizontal = movementbb.horizontal_x * ownerTransform.transform.right;
        movementbb.Vertical = movementbb.vertical_z * ownerTransform.transform.forward;
        Vector3 move = movementbb.Horizontal + movementbb.Vertical;
        move = Vector3.ClampMagnitude(move, 1f);
        controller.Move(move * movementbb.speed * Time.deltaTime);
    }

    public void Exit() { }

}
