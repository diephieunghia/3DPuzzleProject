using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class ArcherAction : MonoBehaviour
{
    public CharacterController characterController;
    public ArcherBlackBoard bb;

    public Transform groundCheck;
    public LayerMask layerMask;
    private void Awake()
    {
        bb = new ArcherBlackBoard();
        bb.groundCheck = groundCheck;
        bb.groundMask = layerMask;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Move();
        Jump();
        DoubleJump();
        ApplyGravity();
        Shoot();
        Dash();
        Debug.Log(bb.velocity.y);
    }

    void GetInput()
    {             
        bb.horizontal_x= Input.GetAxis("Horizontal");
        bb.vertical_z = Input.GetAxis("Vertical");
    }
    void Move()
    {
        bb.Horizontal = transform.right * bb.horizontal_x;
        bb.Vertical = transform.forward * bb.vertical_z;
        Vector3 move = bb.Horizontal + bb.Vertical;
        move = Vector3.ClampMagnitude(move, 1f);
        bb.velocity = move;     
        characterController.Move(move * bb.speed * Time.deltaTime);
    }
    void Jump()
    {
        bb.isGround = Physics.CheckSphere(bb.groundCheck.position, bb.groundDistance, bb.groundMask);
        if (bb.isGround && bb.velocity.y < 0)
        {
            bb.speed = bb.tempSpeed;
            bb.velocity.y = -2f;
            bb.doubleJump = true;
        }      
        if (Input.GetButtonDown("Jump") &&bb.isGround)
        {
            bb.speed = 2f;
            bb.velocity.y = Mathf.Sqrt(bb.jumpHeight *2.0f*bb.gravity);
        }
        
    }
    void DoubleJump()
    {
        if(!bb.isGround&&bb.doubleJump)
        {
            if(Input.GetButtonDown("Jump"))
            {
                bb.doubleJump = false;
                bb.velocity.y = Mathf.Sqrt((bb.jumpHeight+.75f) * 2.0f * bb.gravity);
            }
        }
    }
    void ApplyGravity()
    {
        bb.velocity.y -= bb.gravity * Time.deltaTime;
        characterController.Move(bb.velocity.y * Vector3.up * Time.deltaTime);
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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            bb.dash = true;
        }
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, .4f);
    }
}
