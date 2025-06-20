using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Jump : IState
{
    Transform ownerTransform;
    ArcherBlackBoard movementbb;
    MovementStateHandler statehandler;
    CharacterController controller;

    public Jump(Transform ownerTransform, ArcherBlackBoard movementbb, MovementStateHandler handler, CharacterController characterController)
    {
        this.ownerTransform = ownerTransform;
        this.movementbb = movementbb;
        this.statehandler = handler;
        controller = characterController;
    }
    public void Enter()
    {
        movementbb.jumpVelocity = Mathf.Sqrt(movementbb.jumpHeight * 2.0f * movementbb.gravity);
    }

    public void Execute()
    {  
        Debug.Log(movementbb.isGround);
        if(movementbb.isGround&&movementbb.jumpVelocity < 0f)
        {
            movementbb.jumpVelocity = -2f;           

            if (movementbb.velocity.magnitude > 0.01f)
            {
                statehandler.MoveChangeState(statehandler.move);
                Debug.Log("move to move");
            }
            else
            { 
                statehandler.MoveChangeState(statehandler.idle);
                Debug.Log("move to idle");
            }

        }      
        movementbb.jumpVelocity -= movementbb.gravity * Time.deltaTime;
        controller.Move(movementbb.jumpVelocity * Vector3.up * Time.deltaTime);
        Debug.Log(movementbb.jumpVelocity);
        movementbb.isGround = Physics.CheckSphere(movementbb.groundCheck.position, movementbb.groundDistance, movementbb.groundMask);
    }

    public void Exit()
    {
        
    }
}
