using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionAnim : MonoBehaviour
{
    Animator anim;
    BaseMonster monster;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        monster = GetComponent<BaseMonster>();
        monster.DeathTrigger += Death;
        monster.BodyHit += BodyHit;
        monster.HeadHit += HeadHit;
    }

    // Update is called once per frame
    void Update()
    {
        Speed();
        Attack();
    }
    public void Speed()
    {
        anim.SetFloat("Velocity", monster.MonsterStat.speed);
    }
    public void Attack()
    {
        anim.SetBool("Attack", monster.Attack);
        anim.SetInteger("AttackType", monster.attackType);
    }
    public void Death()
    {
        anim.SetBool("Death", true);      
    }
    public void BodyHit()
    {
        anim.SetTrigger("BodyHit");
    }
    public void HeadHit()
    {
        anim.SetTrigger("HeadHit");
    }
    public void DisableAttack()
    {
        monster.Attack = false;
        monster.axeEnable = true;
    }
    public void DisableAxe()
    {
        monster.axeEnable = false;
    }

}
