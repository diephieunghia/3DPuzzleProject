using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Windows;
[RequireComponent(typeof(SkillHandler))]
public class ArcherAction : MonoBehaviour
{
    public CharacterController characterController;
    public ArcherBlackBoard bb;
    public PlayerInput input;
    public AttackStateHandler attackStateHandler;
    SkillHandler skillHandler;

    public Transform groundCheck;
    public LayerMask groundMask;

    [SerializeField] GameObject arrowHolder;
    public Action arrowFireIce;

    public AudioSource Walk;
    bool walkSoundPlaying = false;
    private void Awake()
    {
        bb = new ArcherBlackBoard();
        bb.groundCheck = groundCheck;
        bb.groundMask= groundMask;  
        skillHandler=GetComponent<SkillHandler>();
        input = new PlayerInput(bb,transform,skillHandler,this);            
    }

    private void Start()
    {
        input.dashActive += Dash;
        input.doubleJumpActive += DoubleJump;
        attackStateHandler = new AttackStateHandler(bb, transform, characterController);
        GameManager.ins.CountDownComplete += ChangeSceneState;
        GameManager.ins.MoveToArea += ExitShop;
        arrowFireIce += FireIceCycle;
    }
    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        ApplyGravity();
        attackStateHandler.HandleAttackState();       
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
        //play walk sound
        if (move.magnitude > 0 && !walkSoundPlaying)
        {
            walkSoundPlaying = true;
            Walk.volume = GameSettings.ins.soundVolume;
            Walk.loop = true;
            Walk.Play();
            Debug.Log("Walk sound play");

        }
        else if (move.magnitude<=0)
        {
            Walk.Stop();
            walkSoundPlaying = false;
        }

    }
    void Jump()
    {
        bb.isGround = Physics.CheckSphere(bb.groundCheck.position, bb.groundDistance, bb.groundMask);
        if (bb.isGround && bb.jumpVelocity < 0)
        {
            bb.speed = bb.tempSpeed;
            bb.jumpVelocity = -2f;
            bb.doubleJump = true;
            input.jumpRelease = false;
        }
    }
    void DoubleJump()
    {
        bb.jumpVelocity = Mathf.Sqrt((bb.jumpHeight + .5f) * 2.0f * bb.gravity);
    }
    void ApplyGravity()
    {
        if (bb.jumpVelocity <= 0.1f)
            bb.jumpVelocity -= bb.gravity * Time.deltaTime * bb.fallMultiplier;
        bb.jumpVelocity -= bb.gravity * Time.deltaTime;
        characterController.Move(bb.jumpVelocity * Vector3.up * Time.deltaTime);
    }
    void Dash()
    {
        if (bb.dash)
            return;
        Vector3 dashVec= transform.right * bb.horizontal_x + transform.forward * bb.vertical_z;
        dashVec= Vector3.ClampMagnitude(dashVec, 1f);
        if (dashVec == Vector3.zero)
        {
            dashVec = transform.forward ;
        }
        dashVec = Vector3.ClampMagnitude(dashVec, 1f);
        SoundManager.ins.PlaySoundOneShot(SoundType.Dash, 1);
        StartCoroutine("HandleDash", dashVec);
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
    //enter exit shop
    void ChangeSceneState()
    {
        bb.storeAction = !bb.storeAction;
        if (attackStateHandler.CurrentState == attackStateHandler.shoot)       
            bb.aiming = ArcherBlackBoard.Aim.Shoot;       
        //Character anim change back to idle
        else if (attackStateHandler.CurrentState == attackStateHandler.eSkill)
        {
            bb.skill = false;
            bb.eSkill = false;
            bb.aiming = ArcherBlackBoard.Aim.Idle;
            HandleAnim.ins.DisableEShoot();
        }
    }
    void ExitShop()
    {
        bb.storeAction = false;
    }

    //Arrow Fire Ice
    void FireIceCycle()
    {
        Arrow[] arrow=arrowHolder.GetComponentsInChildren<Arrow>();
        foreach(Arrow a in arrow)
        {
                if (!a.Fire && a.Ice)
                    a.FlipFireIce(true);
                else if (!a.Ice && a.Fire)
                    a.FlipFireIce(false);
                else if (!a.Fire && !a.Ice)
                    a.FlipFireIce(true);          
        }

    }

}
