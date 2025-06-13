using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherMovement : MonoBehaviour
{
    public CharacterController characterController;

    //move
    float horizontal_x;
    float vertical_z;
    public float speed=12f;
   
    //jump
    bool doubleJump = true;
    public float gravity = 9.81f ;
    public float jumpHeight = 3f;   
    Vector3 velocity;
    public Transform groundCheck;
    public LayerMask groundMask;
    public float groundDistance = .4f;

    bool isGround = true;
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        horizontal_x = Input.GetAxis("Horizontal");
        vertical_z = Input.GetAxis("Vertical");
        
        Vector3 move = transform.right*horizontal_x+transform.forward*vertical_z;
        move = Vector3.ClampMagnitude(move, 1f);

        characterController.Move(move * speed * Time.deltaTime);
    }
    void Jump()
    {
        isGround = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGround && velocity.y < 0)
        {
            velocity.y = -2f;
            doubleJump = true;
        }
        
        if (Input.GetButtonDown("Jump") &&isGround)
        {
            velocity.y = Mathf.Sqrt(jumpHeight *2.0f*gravity);
        }
        //apply gravity
        velocity.y -= gravity * Time.deltaTime;
        characterController.Move(velocity.y * Vector3.up*Time.deltaTime);
    }
    void DoubleJump()
    {
        if(!isGround&&doubleJump)
        {
            if(Input.GetButtonDown("Jump"))
            { }
        }
    }
    void Shoot()
    {

    }
    void Dash()
    {

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
    }
}
