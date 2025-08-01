using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SO", menuName = "ScriptableObjects/BaseCharacterStats")]

public class SO_Char : ScriptableObject
{
    //level experience
    public int level = 1;
    public float maxEXP=100f;
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

    public enum Aim
    {
        Idle,
        Hold,
        Shoot,
        Cancel
    }
    //Shoot
    [Header("Shoot")]
    public Aim aiming=Aim.Cancel;

    public float hitDistance = 1f;
    public float force = 20f;
    public float maxForce = 120f;
    public float shootRate = 0f;

    public float shootRateMax = .5f;
    public bool allowShoot = false;
    public delegate void HandleDraw(Aim aimMode);
    public HandleDraw DrawArrow;
    public GameObject[] currentArrow= { null, null, null };
    public int arrowCount = 3;

    //damage
    public float damage = 20f;
    public float maxDamage = 125f;

    //Dash
    public bool dash = true;
    public float dashSpeed = 9f;
    public float dashDuration = .3f;
    public float dashCoolDown = 2f;

    //Skill Index
    public int[] skillIndex = { 0, 1, 2, 3, 4 };
    public bool skill = false;
    //E
    public bool eSkill = false;
    public float eDuration = 3f;
    public float eCoolDown = 8f;
    public float animSpeed = 12f;
}
