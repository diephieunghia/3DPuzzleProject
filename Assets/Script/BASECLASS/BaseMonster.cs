using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseMonster : MonoBehaviour
{
    [SerializeField] SO_Mons stat;

    public bool Attack { get { return stat.attack; } set{ stat.attack = value; } }
}
