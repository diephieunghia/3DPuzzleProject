using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;


public class PlayerInput
{
    public ArcherBlackBoard bbInput;
    public bool jumpRelease = false;
    Transform position;
    public Action dashActive;
    public Action doubleJumpActive;
    public SkillHandler inputSkill;
    public PlayerInput(ArcherBlackBoard bb,Transform playerPosition,SkillHandler skillHandler)
    {
        bbInput = bb;
        position = playerPosition;
        inputSkill= skillHandler;
    }

    //handle called in update method
    public void HandleInput()
    {
        GetInput();
        Jump();
        DoubleJump();
        Dash();
        HoldDraw();
        SkillE();
    }

    void GetInput()
    {
        bbInput.horizontal_x= Input.GetAxis("Horizontal");
        bbInput.vertical_z = Input.GetAxis("Vertical");       
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && bbInput.isGround)
        {
            bbInput.isGround = false;
            bbInput.jumpVelocity = Mathf.Sqrt(bbInput.jumpHeight * 2.0f * bbInput.gravity);
        }
        else if (Input.GetButtonUp("Jump"))
            jumpRelease = true;
    }
    void DoubleJump()
    {
        if (bbInput.doubleJump && !bbInput.isGround)
        {
            if (Input.GetButtonDown("Jump") && jumpRelease)
            {            
                bbInput.doubleJump = false;              
                jumpRelease = true;
                doubleJumpActive.Invoke();
                
            }
        }
    }
    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)&&bbInput.dash)
        {          
            bbInput.dash = false;
            dashActive.Invoke();
            inputSkill.skillDelegate.Invoke(bbInput.dashCoolDown, bbInput.skillIndex[0]);
        }       
    }
    void HoldDraw()
    {
        if (Input.GetButtonDown("Fire1"))       
            bbInput.aiming = ArcherBlackBoard.Aim.Hold;
        
        else if(Input.GetButtonUp("Fire1"))        
            bbInput.aiming = ArcherBlackBoard.Aim.Shoot;

        if(!bbInput.eSkill)
            if (Input.GetButtonDown("Fire2"))
                bbInput.aiming = ArcherBlackBoard.Aim.Cancel;
        
    }
    //skill E increase fire rate
    void SkillE()
    {
        if (Input.GetKeyDown(KeyCode.E)&&!bbInput.skill&&!bbInput.eSkill)
        {
            bbInput.eSkill = true;
            bbInput.skill = true;
            inputSkill.skillDelegate.Invoke(bbInput.eCoolDown, bbInput.skillIndex[1]);
        }
    }


    //Cheat

}
