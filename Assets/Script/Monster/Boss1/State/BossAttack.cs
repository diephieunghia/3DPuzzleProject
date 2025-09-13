using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BaseMonster))]
public class BossAttack : MonoBehaviour
{
    BaseMonster baseMonster;
    float attackDuration;

    public float attackRange;
    public Transform range;

    Coroutine current;
    public float checkPlayerInRangeInterval;
    float checkTemp;
    public LayerMask player;
    Collider[] playerdetect;
    private void Awake()
    {
        baseMonster = GetComponent<BaseMonster>();
    }
    // Start is called before the first frame update
    void Start()
    {
        attackDuration=baseMonster.MonsterStat.attackRate;
        current = StartCoroutine(AttackDurationEnd());
        checkTemp = checkPlayerInRangeInterval;
        playerdetect = new Collider[1];
    }

    // Update is called once per frame
    void Update()
    {
        checkTemp -= Time.deltaTime;
        if (checkTemp < 0) {
            int _numcollider = Physics.OverlapSphereNonAlloc(transform.position, attackRange,playerdetect, player);
            if (_numcollider == 0) StopAllCoroutines();
            else current=StartCoroutine(AttackDurationEnd());
        }
    }
    IEnumerator AttackDurationEnd()
    {
        yield return new WaitForSeconds(attackDuration);
        baseMonster.Attack = false;
        StartCoroutine(AttackCoolDown());
    }
    IEnumerator AttackCoolDown()
    {
        yield return new WaitForSeconds(baseMonster.MonsterStat.baseCoolDown);
        baseMonster.Attack = true;
        StartCoroutine (AttackDurationEnd());

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    public void SpawnProjectile()
    {

    }
}
