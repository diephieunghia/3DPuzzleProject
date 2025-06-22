using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ArcherAction),typeof(Animation))]
public class HandleAnim : MonoBehaviour
{
    ArcherAction archerAction;
    [SerializeField] Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        archerAction = GetComponent<ArcherAction>();
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
        if (archerAction.bb.aiming == ArcherBlackBoard.Aim.Hold)
            anim.SetBool("Draw", true);
        else
            anim.SetBool("Draw", false);
    }
}
