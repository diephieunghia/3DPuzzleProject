using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ArcherBlackBoard;

[RequireComponent(typeof(ArcherAction),typeof(Animation))]
public class HandleAnim : MonoBehaviour
{
    ArcherAction archerAction;
    [SerializeField] Animator anim;
<<<<<<< Updated upstream:Assets/Script/ArcherLevel/HandleAnim.cs
    // Start is called before the first frame update
    void Start()
    {
        archerAction = GetComponent<ArcherAction>();
=======
    public AnimationClip standDraw;
    public AnimationClip overDraw;

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
        anim= GetComponentInChildren<Animator>();
        RuntimeAnimatorController controller = anim.runtimeAnimatorController;
        foreach (AnimationClip clip in controller.animationClips)
        {
            if (clip.name == "Stand Draw")
                standDraw = clip;
            else if (clip.name == "AimOverDraw")
                overDraw = clip;
        }      

>>>>>>> Stashed changes:Assets/Script/Character/Archer/ArcherLevel/HandleAnim.cs
    }

    // Update is called once per frame
    void Update()
    {
        SetIdle();
        SetMovement();
        SetDash();
        SetAttack();
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
    void SetAttack()
    {
        if (archerAction.bb.aiming)
            anim.SetBool("Draw", true);
<<<<<<< Updated upstream:Assets/Script/ArcherLevel/HandleAnim.cs
        else
            anim.SetBool("Draw", false);
=======
            anim.SetFloat("DrawSpeed", 3f);
        }
    }
    public void DisableAttack()
    {
        anim.SetBool("Draw", false);
    }
    public void SetAllowShoot()
    {
        archerAction.bb.allowShoot = true;
        Debug.Log("allow shoot");
>>>>>>> Stashed changes:Assets/Script/Character/Archer/ArcherLevel/HandleAnim.cs
    }
    public void NotAllowShoot()
    {
        archerAction.bb.allowShoot = false;
        Debug.Log("not allow shoot");
    }
    public void GetArrow()
    {
        archerAction.bb.currentArrow= ArrowPool.ins.GetObject();
    }

    public void StateFinished()
    {
      
    }
    public bool CheckClip(AnimationClip currentClip)
    {
        AnimatorClipInfo[] clips = anim.GetCurrentAnimatorClipInfo(0);
        foreach (var info in clips)        
            if (info.clip == currentClip)
            {
                return true;
            }
        return false;
    }

    float normalizedTime;
    public bool CheckClipHasEnd(AnimationClip currentClip)
    {

        if (anim.GetCurrentAnimatorClipInfo(1)[0].clip == currentClip)
            normalizedTime = anim.GetCurrentAnimatorStateInfo(1).normalizedTime;
        if (normalizedTime >= 0.999f) return true;
        return false;
    }

}
