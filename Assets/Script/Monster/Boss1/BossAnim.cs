using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BaseMonster))]
public class BossAnim : MonoBehaviour
{
    BaseMonster baseMonster;
    public 
    Animator animator;
    private void Awake()
    {
        baseMonster=GetComponent<BaseMonster>();
        animator=GetComponent<Animator>(); 
        baseMonster.DeathTrigger += Death;
    }
    
    public void SetRunWalk(float index)
    {
        animator.SetFloat("Velocity", index);
    }
    public void SetAttack(bool attack)
    {
        animator.SetBool("Attack", attack);
    }
    public void Reload()
    {
        animator.SetBool("Reload", true);
    }
    public void Death()
    {
        animator.SetLayerWeight(1, 0);
        animator.SetBool("Death", true);
       
    }
}
