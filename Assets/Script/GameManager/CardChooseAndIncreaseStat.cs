using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    //coins text
    public TextMeshProUGUI coinsDisplay;
    private void OnTriggerEnter(Collider other)
    {
        image.color= highlight;
        if (other.gameObject.CompareTag("Player"))
        { 
            playerInRange = true;
            archer=other.GetComponent<ArcherBase>();
            GameManager.ins.BuyItem += BuyItem;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        image.color = normal;
        playerInRange = false;
        GameManager.ins.BuyItem -= BuyItem;

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

    void BuyItem()
    {
        Debug.Log("In Buy Item");
        if (playerInRange && GameManager.ins.playerPressBuy)
        {
            if (GameManager.ins.coinsHeld < item.cost)
            {
                //uimanager play text anim

                Debug.Log("Not enough coins");
                return;
            }
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
            else if (item.type == ItemType.Skill)
            {
                archer.Skill(item);
            }
            //reduce coins after buy
            GameManager.ins.UpdateCoinsAmount?.Invoke(-item.cost);
            //reduce count for item if item has 0 then substract from total item
            item.ReduceCount();
            GameManager.ins.playerPressBuy = false;
            //remove from event after buy successfully
            GameManager.ins.BuyItem -= BuyItem;
            //Disable Card
            DisableCard();
        }
    }
}
