using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BaseMonster),typeof(BossAnim))]
public class BossAttack : MonoBehaviour
{
    BaseMonster baseMonster;
    BossAnim bossAnim;
    float attackDuration;

    public float attackRange;

    Coroutine current;
    public float checkPlayerInRangeInterval;
    float checkTemp;
    public LayerMask player;
    Collider[] playerdetect;
    //projectile holder
    public Transform projectileHolder;
    private void Awake()
    {
        baseMonster = GetComponent<BaseMonster>();
        bossAnim = GetComponent<BossAnim>();
    }
    // Start is called before the first frame update
    void Start()
    {
        attackDuration=baseMonster.MonsterStat.attackRate;
        current = StartCoroutine(AttackDurationEnd());
        checkTemp = checkPlayerInRangeInterval;
        playerdetect = new Collider[5];
    }

    // Update is called once per frame
    void Update()
    {
        checkTemp -= Time.deltaTime;
        if (checkTemp < 0) {
            int _numcollider = Physics.OverlapSphereNonAlloc(transform.position, attackRange,playerdetect, player);
            if (_numcollider == 0||baseMonster.Death) StopAllCoroutines();
            else
            {
                foreach (Collider collider in playerdetect) {
                    if (collider.CompareTag("Player"))
                    {
                        if (current == null)
                            current = StartCoroutine(AttackDurationEnd());
                        break;
                    }
                    else break;
                }
            }               
        }
    }
    IEnumerator AttackDurationEnd()
    {
        yield return new WaitForSeconds(attackDuration);
        baseMonster.Attack = false;
        bossAnim.SetAttack(baseMonster.Attack);
        StartCoroutine(AttackCoolDown());
    }
    IEnumerator AttackCoolDown()
    {
        yield return new WaitForSeconds(baseMonster.MonsterStat.baseCoolDown);
        baseMonster.Attack = true;
        bossAnim.SetAttack(baseMonster.Attack);
        StartCoroutine (AttackDurationEnd());

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    //using in shooting Anim
    public void SpawnProjectile()
    {
        GameObject projectile= MageProjectilePool.ins.GetObject(); 
        projectile.transform.position=projectileHolder.transform.position;
        projectile.GetComponent<ProjectileMove>().GetDirection(transform.forward, transform.rotation);
        projectile.GetComponent<FireBallDetection>().Damage = baseMonster.MonsterStat.damage;
        
    }
}
