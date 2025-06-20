using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput 
{

    public ArcherBlackBoard bbInput;
    
    public PlayerInput(ArcherBlackBoard bb)
    {
        bbInput = bb;
    }

    //handle called in update method
    public void HandleInput()
    {
        GetInput();
        Jump();
        DoubleJump();
    }

    void GetInput()
    {
        bbInput.horizontal_x= Input.GetAxis("Horizontal");
        bbInput.vertical_z = Input.GetAxis("Vertical");
        bbInput.velocity=Vector3.right*bbInput.horizontal_x+Vector3.forward*bbInput.vertical_z;
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump")&&bbInput.isGround)
        {
            bbInput.isGround = false;
        }
    }
    void DoubleJump()
    {
        if (bbInput.doubleJump && !bbInput.isGround)
        {
            if (Input.GetButtonDown("Jump"))
                bbInput.doubleJump = false;
        }
    }

}
