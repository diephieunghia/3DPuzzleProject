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
        Dash();
    }

    void GetInput()
    {
        bbInput.horizontal_x= Input.GetAxis("Horizontal");
        bbInput.vertical_z = Input.GetAxis("Vertical");       
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump")&&bbInput.isGround)
        {
            bbInput.isGround = false;
            bbInput.jumpVelocity = Mathf.Sqrt((bbInput.jumpHeight-2.5f) * 2.0f * bbInput.gravity);
        }
    }
    void DoubleJump()
    {
        if (bbInput.doubleJump && !bbInput.isGround)
        {
            if (Input.GetButtonDown("Jump"))
            {               
                bbInput.doubleJump = false;
                bbInput.jumpVelocity = Mathf.Sqrt((bbInput.jumpHeight + .75f) * 2.0f * bbInput.gravity);
            }
        }
    }
    void Dash()
    {

    }

}
