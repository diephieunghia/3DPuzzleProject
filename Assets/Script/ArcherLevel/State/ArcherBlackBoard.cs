using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherBlackBoard 
{
    //move
    [Header("Move")]
    public float horizontal_x;
    public float vertical_z;
    public float speed = 5f;
    public float tempSpeed = 5f;

    public Vector3 velocity;
    public Vector3 Horizontal;
    public Vector3 Vertical;
    //jump
    [Header("Jump Param")]
    public bool doubleJump = true;
    public float gravity = 9.81f;
    public float jumpHeight = 2.8f;

    public Transform groundCheck;
    public LayerMask groundMask;
    public float groundDistance = .4f;
    public bool isGround = true;
    public float jumpVelocity=-2f;

    //Dash
    public bool dash = true;
    public float dashSpeed = 9f;
    public float dashDuration = .3f;  

    //Shoot
    [Header("Shoot")]
    public bool aiming = false;


}
