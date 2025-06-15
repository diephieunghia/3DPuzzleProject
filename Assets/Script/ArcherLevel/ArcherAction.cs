using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class ArcherAction : MonoBehaviour
{
    public CharacterController characterController;

    //move
    [Header("Move")]
    float horizontal_x;
    float vertical_z;
    bool froze = false;
    public float speed=5f;
    float tempSpeed = 5f;

    public Vector3 Horizontal_x;
    public Vector3 Vertical_z;
    public float Speed { get { return speed; }  }
    public Vector3 Velocity;
    //jump
    [Header("Jump Param")]
    bool doubleJump = true;
    public float gravity = 9.81f ;
    public float jumpHeight = 3f;   
    Vector3 velocity;
    public Transform groundCheck;
    public LayerMask groundMask;
    public float groundDistance = .4f;
    bool isGround = true;
   

    //Dash
    bool dash = true;
    public float dashSpeed=9f;
    public float dashDuration = .3f;

    public bool DashVar {  get { return dash; } }

    //Shoot
    [Header("Shoot")]
    bool aiming = false;

    public bool Aiming { get {  return aiming; } }

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
    }

    void GetInput()
    {
        if (froze)
        {
            horizontal_x = 0;
            vertical_z = 0;
            return;
        }
        horizontal_x = Input.GetAxis("Horizontal");
        vertical_z = Input.GetAxis("Vertical");

    }
    void Move()
    {
        Horizontal_x = transform.right * horizontal_x;
        Vertical_z = transform.forward * vertical_z;
        Vector3 move = Horizontal_x + Vertical_z;
        move = Vector3.ClampMagnitude(move, 1f);
        Velocity = move;
        characterController.Move(move * speed * Time.deltaTime);
    }
    void Jump()
    {
        isGround = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGround && velocity.y < 0)
        {
            froze = false;
            velocity.y = -2f;
            doubleJump = true;
        }
        else if (!isGround && velocity.y < -0.5f)        
            froze = true;
    
        
        if (Input.GetButtonDown("Jump") &&isGround)
        {
            
            velocity.y = Mathf.Sqrt(jumpHeight *2.0f*gravity);
        }
        
    }
    void DoubleJump()
    {
        if(!isGround&&doubleJump)
        {
            if(Input.GetButtonDown("Jump"))
            {
                doubleJump = false;
                velocity.y = Mathf.Sqrt((jumpHeight+.75f) * 2.0f * gravity);
            }
        }
    }
    void ApplyGravity()
    {
        velocity.y -= gravity * Time.deltaTime;
        characterController.Move(velocity.y * Vector3.up * Time.deltaTime);
    }
    void Shoot()
    {
        if(Input.GetButtonDown("Fire1"))
        {
           aiming = true;
           StartCoroutine("DecreaseSpeedAim");
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            aiming = false;
            StopCoroutine("DecreaseSpeedAim");
            speed = tempSpeed;
        }
    }
    IEnumerator DecreaseSpeedAim()
    {
        while (speed >= tempSpeed / 5f)
        { 
            speed -= 0.08f; 
            yield return new WaitForSeconds(.01f);
        }
        yield return null;
    }

    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            dash = true;
        }
        if (!dash)
            return;
        Vector3 dashVec= transform.right * horizontal_x + transform.forward * vertical_z;
        dashVec= Vector3.ClampMagnitude(dashVec, 1f);
        if (dashVec == Vector3.zero)
        {
            dashVec = transform.forward ;
        }
            
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            froze = true;
            dash = false;          
            dashVec = Vector3.ClampMagnitude(dashVec, 1f);
            StartCoroutine("HandleDash", dashVec);           
        }
        
    }
    IEnumerator HandleDash(Vector3 dashVec)
    {
        float startTime = Time.time;
        while (Time.time < startTime + dashDuration)
        {
            characterController.Move(dashVec * dashSpeed * Time.deltaTime);
            yield return null;
        }
        if (isGround)
            froze = false;
       
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
    }
}
