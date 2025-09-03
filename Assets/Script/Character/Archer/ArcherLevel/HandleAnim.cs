using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleAnim : MonoBehaviour
{
    ArcherAction archerAction;
    [SerializeField] Animator anim;

    public static HandleAnim ins;
    private void Awake()
    {
        if (ins != null && ins != this)
            Destroy(this);
        else
            ins = this;
    }

    void Start()
    {
        archerAction = GetComponentInParent<ArcherAction>();
    }

    // Update is called once per frame
    void Update()
    {
        SetIdle();
        SetMovement();
        SetDash();
    }
    void SetIdle()
    {
        int randIdle = Random.Range(1, 4);
        anim.SetInteger("IdleNum",randIdle);
    }
    void SetMovement()
    {
        float horizontal = Mathf.Clamp(archerAction.bb.Horizontal.x, -1, 1);
        float vertical = Mathf.Clamp(archerAction.bb.Vertical.z, -1, 1);
        float velocity = Mathf.Clamp(archerAction.bb.velocity.magnitude, 0, 1);
        anim.SetFloat("Horizontal", horizontal);
        anim.SetFloat("Vertical", vertical);
        anim.SetFloat("Velocity",velocity);

    }
    void SetDash()
    {
        if(archerAction.bb.dash)
            anim.SetTrigger("Dash");
    }
    public void SetAttackAnim()
    {
        if (archerAction.bb.aiming == ArcherBlackBoard.Aim.Hold )
        {
            anim.SetBool("Draw", true);           
        }
            
    } 

    public void DisableAttack()
    {
        anim.SetBool("Draw", false);
    }
    public void SetAllowShoot()
    {
        archerAction.bb.allowShoot = true;
    }
    public bool WaitForReloadAnim()
    {
        if((anim.GetCurrentAnimatorStateInfo(1).normalizedTime > 0.89f && anim.GetCurrentAnimatorStateInfo(1).IsName("StandDraw")))
        {
            return true;
        }
        return false;
    }
    //Eskill
    public void EShoot()
    {
            anim.SetBool("ESkill", true);                    
    }
    public void EAnim()
    {
            anim.SetFloat("OverDraw", archerAction.bb.animSpeed);       
    }
    public bool AnimOverDrawEnd(float time)
    {
        if ((anim.GetCurrentAnimatorStateInfo(1).normalizedTime > time && anim.GetCurrentAnimatorStateInfo(1).IsName("ESkill")))
        {
            return true;
        }
        return false;       
    }
    public void DisableEShoot()
    {
        anim.SetBool("ESkill", false);
    }
    public bool ReloadESkill(float time)
    {
        if ((anim.GetCurrentAnimatorStateInfo(1).normalizedTime > time && anim.GetCurrentAnimatorStateInfo(1).IsName("StandDraw")))
        {
            return true;
        }
        return false;
    }
    public void ManualReturnIdle()
    {
        if (!archerAction.bb.skill)
            if (archerAction.bb.eSkill) { 
                archerAction.bb.eSkill = false; 
                anim.SetBool("ESkill", false);
            }
        

    }

}
