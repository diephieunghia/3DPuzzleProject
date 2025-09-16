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
        baseMonster.DeathTrigger += SetDeath;
    }
    
    public void SetRunWalk(float index)
    {
        animator.SetFloat("Velocity", index);
    }
    public void SetDeath()
    {
        animator.SetBool("Death", true);
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
        animator.SetBool("Death", true);
        animator.SetLayerWeight(1, 0);
    }
}
