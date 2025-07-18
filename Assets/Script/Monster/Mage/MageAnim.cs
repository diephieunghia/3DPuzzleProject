using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BaseMonster))]
public class MageAnim : MonoBehaviour
{
    Animator anim;
    BaseMonster monster;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        monster = GetComponent<BaseMonster>();
        monster.DeathTrigger += Death;
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
        anim.SetBool("Attack", monster.MonsterStat.attack);
    }
    public void Death()
    {
        anim.SetBool("Death",monster.MonsterStat.death);
        Debug.Log("Trigger Death");
    }
}
