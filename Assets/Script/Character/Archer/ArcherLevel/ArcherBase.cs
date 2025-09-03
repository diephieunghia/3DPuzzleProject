using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;
using static IDamageable;
[RequireComponent(typeof(ArcherAction))]
public class ArcherBase : BaseChar
{
    ArcherBlackBoard bb;
    public ArcherBlackBoard BB => bb;
    ArcherAction archer;

    float currentEXP = 0;
    float currentHealth;

    bool invicible = false;
    float inviTime = 1.25f;
    protected override void Awake()
    {
        base.Awake();
        archer = GetComponent<ArcherAction>();
        
    }
    private void Start()
    {
        GameManager.ins.LevelChange += IncreaseLevel;
        GameManager.ins.UpdateCoinsAmount += UpdateCoinsHeld;
        bb = GetComponent<ArcherAction>().bb;
        currentHealth = bb.health;
        //assign health at the start of the game to the ui
        UIManager.ins.SetHealth(currentHealth,bb.health);
    }
    //damage 
    public override void TakeDamage(float damage, Vector3 hitPoint, Vector3 hitDirection, GameObject attacker, IDamageable.Body hitPart)
    {
        float tempDamage = damage;
        if (invicible)
        {
            return; 
        }
        Debug.Log("Hit");
        //reduce health
        currentHealth = Mathf.Clamp(currentHealth - tempDamage *(1-bb.armor/100), 0, bb.health);
        //update to UI
        UIManager.ins.SetHealth(currentHealth,bb.health);
        //invicible for x seconds, negate damage, start coroutine here
        StartCoroutine(GodMode());
        
    }
    IEnumerator GodMode()
    {
        if (invicible) yield break;
        invicible = true;
        yield return new WaitForSeconds(inviTime);
        invicible = false;
    }
    //Level Up
    protected override void IncreaseLevel(float value,float coins)
    {
        currentEXP += value;
        while (currentEXP >= bb.maxEXP) { 
            currentEXP -= bb.maxEXP;
            bb.level += 1;
            //increase coins earn multiplier
            bb.coinsMultiplier += 0.01f;
            UIManager.ins.LevelChange(bb.maxEXP, bb.maxEXP, bb.level);           
            //max exp will increase here         
            bb.maxEXP += 10f;
            //send message to increase stat
        }
        UIManager.ins.LevelChange(currentEXP, bb.maxEXP, bb.level);
        //coins earn and update to ui and manager
        UpdateCoinsHeld(coins * bb.coinsMultiplier);
    }
    //StatUp
    public void StatUP(SO_Item item)
    {
        bb.dashCoolDown += bb.dashCoolDown * item.dashCoolDown;
        bb.dashCoolDown = Mathf.Max(0.1f, bb.dashCoolDown);
        bb.health += item.health;
        bb.health=Mathf.Max(1, bb.health);
        bb.armor += bb.armor * item.armor/10;
        bb.damage+=bb.damage * item.damage;
        bb.maxDamage+=item.maxDamage;
        bb.speed += item.speed;
        bb.tempSpeed+=item.tempSpeed;

    }
    public void Special(SO_Item item)
    {
        
    }
    public void Skill(SO_Item item)
    {
        int fireIcetemp=-1;
        //update arrow from 1 to 3
        if (item.upgradeType == "ArrowCount")
        {
            bb.arrowCount = item.arrowCount;
            float z = 0;
            //disable
            for (int i = 0; i < bb.arrowCount; i++)
            {
                if (bb.currentArrow[i] != null)
                {
                    Arrow arrow = bb.currentArrow[i].GetComponent<Arrow>();
                    if (arrow.Fire) fireIcetemp = 1;
                    else if (arrow.Ice) fireIcetemp = 0;                    
                    ArrowPool.ins.ReturnObject(bb.currentArrow[i]);
                }
                if (i == 1) z = -15;
                else if (i == 2) z = 15;
                bb.currentArrow[i] = ArrowPool.ins.GetObject();
                bb.currentArrow[i].transform.localRotation = Quaternion.Euler(0, z, 0);
                if (fireIcetemp != -1)
                    if (fireIcetemp == 1)
                        bb.currentArrow[i].GetComponent<Arrow>().FlipFireIce(true);
                    else
                        bb.currentArrow[i].GetComponent<Arrow>().FlipFireIce(false);
            }
        }
        //update arrow to have fire ice effect
        else if (item.upgradeType == "FireIce")
        {
            bb.qSkill = false ;
            bb.qSkillUnlocked = true;
            //set fire to future spawn arrow
            ArrowPool.ins.fireIce = 1;
        }

    }

    public void getCharStat()
    {
        GameManager.ins.GetCharStat?.Invoke(bb);
    }
    public void ItemGetStatOnceAtSpawn(SO_Item item)
    {
        
    }
    void UpdateCoinsHeld(float value)
    {        
        bb.coinsHeld += value;
        //let Game manager get coins held value
        GameManager.ins.coinsHeld = bb.coinsHeld;
        //update coins UI
        UIManager.ins.CoinsChange(value, bb.coinsHeld);
    }
}
