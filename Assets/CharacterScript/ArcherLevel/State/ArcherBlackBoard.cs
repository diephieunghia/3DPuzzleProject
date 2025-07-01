using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherBlackBoard 
{
    //move
    [Header("Move")]
    public float horizontal_x;
    public float vertical_z;
    public float speed = 8f;
    public float tempSpeed = 8f;

    public Vector3 velocity;
    public Vector3 Horizontal;
    public Vector3 Vertical;
    //jump
    [Header("Jump Param")]
    public bool doubleJump = true;
    public float gravity = 9.81f;
    public float jumpHeight = 2f;

    public Transform groundCheck;
    public LayerMask groundMask;
    public float groundDistance = .4f;
    public bool isGround = true;
    public float jumpVelocity=-2f;

    public float fallMultiplier = 1.7f;

    //Dash
    public bool dash = true;
    public float dashSpeed = 9f;
    public float dashDuration = .3f;  

    public enum Aim
    {
        Hold,
        Shoot,
        Cancel
    }
    //Shoot
    [Header("Shoot")]
    public Aim aiming=Aim.Cancel;

    public float hitDistance = 1f;
    public float force = .8f;
    public float maxForce = 70f;
    public float shootRate = 0f;

    public float shootRateMax = .5f;
    public bool allowShoot = false;
    public delegate void HandleDraw(Aim aimMode);
    public HandleDraw DrawArrow;
    public GameObject currentArrow;
}
