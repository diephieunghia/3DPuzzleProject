using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BaseMonster))]
public class BossAnim : MonoBehaviour
{
    BaseMonster baseMonster;
    private void Awake()
    {
        baseMonster=GetComponent<BaseMonster>();
    }

}
