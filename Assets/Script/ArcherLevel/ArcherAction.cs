using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class ArcherAction : MonoBehaviour
{
    public CharacterController characterController;
    public ArcherBlackBoard bb;
    public PlayerInput input;
    public AttackStateHandler attackStateHandler;

    public Transform groundCheck;
    public LayerMask groundMask;
    private void Awake()
    {
        bb = new ArcherBlackBoard();
        bb.groundCheck = groundCheck;
        bb.groundMask= groundMask;  
        attackStateHandler = new AttackStateHandler(bb, transform, characterController);
        input = new PlayerInput(bb);
    }

    // Update is called once per frame
    void Update()
    {      
        Move();
        Jump();
        DoubleJump();
        ApplyGravity();
        Dash();
        //attackStateHandler.HandleAttackState();      
        
    }
    private void LateUpdate()
    {
        input.HandleInput();
    }
    void Move()
    {   
        bb.Horizontal = transform.right * bb.horizontal_x;
        bb.Vertical = transform.forward * bb.vertical_z;
        Vector3 move = bb.Horizontal + bb.Vertical;
        move = Vector3.ClampMagnitude(move, 1f);
        bb.velocity = move;
        characterController.Move(bb.velocity * bb.speed * Time.deltaTime);
    }
    void Jump()
    {
        bb.isGround = Physics.CheckSphere(bb.groundCheck.position, bb.groundDistance, bb.groundMask);
        if (bb.isGround && bb.jumpVelocity < 0)
        {
            bb.jumpVelocity = -2f;
            bb.doubleJump = true;
        }
    }
    void DoubleJump()
    {
    }
    void ApplyGravity()
    {
        bb.jumpVelocity -= bb.gravity * Time.deltaTime;
        characterController.Move(bb.jumpVelocity * Vector3.up * Time.deltaTime);
    }
    void Shoot()
    {
        if(Input.GetButtonDown("Fire1"))
        {
           bb.aiming = true;
           StartCoroutine("DecreaseSpeedAim");
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            bb.aiming = false;
            StopCoroutine("DecreaseSpeedAim");
            bb.speed = bb.tempSpeed;
        }
    }
    IEnumerator DecreaseSpeedAim()
    {
        while (bb.speed >= bb.tempSpeed / 5f)
        { 
            bb.speed -= 0.08f; 
            yield return new WaitForSeconds(.01f);
        }
        yield return null;
    }

    void Dash()
    {
        if (!bb.dash)
            return;
        Vector3 dashVec= transform.right * bb.horizontal_x + transform.forward * bb.vertical_z;
        dashVec= Vector3.ClampMagnitude(dashVec, 1f);
        if (dashVec == Vector3.zero)
        {
            dashVec = transform.forward ;
        }
            
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            bb.dash = false;          
            dashVec = Vector3.ClampMagnitude(dashVec, 1f);
            StartCoroutine("HandleDash", dashVec);           
        }
        
    }
    IEnumerator HandleDash(Vector3 dashVec)
    {
        float startTime = Time.time;
        while (Time.time < startTime + bb.dashDuration)
        {
            characterController.Move(dashVec * bb.dashSpeed * Time.deltaTime);
            yield return null;
        }      
    }
}
