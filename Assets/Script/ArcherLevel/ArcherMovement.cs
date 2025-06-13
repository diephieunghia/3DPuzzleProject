using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherMovement : MonoBehaviour
{
    public CharacterController characterController;

    public float speed=12f;
    public float gravity=9.81f*2;
    public float jumpHeight = 3f;

    bool doubleJump = true;

    float horizontal_x; 
    float vertical_z;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontal_x = Input.GetAxis("Horizontal");
        vertical_z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * horizontal_x + transform.forward * vertical_z ;

        characterController.Move(move * speed * Time.deltaTime);

    }
}
