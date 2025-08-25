using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CardChooseAndIncreaseStat : MonoBehaviour
{
    //Image for highlight
    SO_Item item;
    public Image image;

    public Color highlight;
    public Color normal;

    ArcherBase archer;
    bool playerInRange = false;

    [SerializeField] BoxCollider ownCollider;
    public GameObject card;

    private void Update()
    {
        
        if (playerInRange&&GameManager.ins.playerPressBuy)
        {
            int rand = item.RandomInt();
            if (item.type == ItemType.Stat)
                archer.StatUP(item);
            //special to upgrade arrow, fireice
            else if (item.type == ItemType.Special)
                archer.Special(item);
            else if (item.type == ItemType.Passive)
            {
                item.SpawnObject(true, rand, archer.BB);
            }
            else if (item.type==ItemType.Skill)
            {
                archer.Skill(item);
            }
                
            item.ReduceCount();
            GameManager.ins.playerPressBuy = false;
            //Disable Card
            DisableCard();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        image.color= highlight;
        if (other.gameObject.CompareTag("Player"))
        { 
            playerInRange = true;
            archer=other.GetComponent<ArcherBase>();

        }
    }
    private void OnTriggerExit(Collider other)
    {
        image.color = normal;
        playerInRange = false;

    }
    public void AssignItem(SO_Item _item)
    {
        item = _item;
    }

    void DisableCard()
    {
        ownCollider.enabled = false;
        card.SetActive(false);

    }
}
