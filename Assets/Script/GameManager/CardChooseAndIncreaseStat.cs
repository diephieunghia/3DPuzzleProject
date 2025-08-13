using System.Collections;
using System.Collections.Generic;
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
            Debug.Log("Buy Item: "+item.name);
            if (item.type == ItemType.Stat)
                archer.StatUP(item);
            else if (item.type == ItemType.Special)
                archer.Special(item);
            else if (item.type == ItemType.Passive)
            {
                Debug.Log("Passive Item");
            }
            else
            {
                item.SpawnObject(true, rand,archer.BB);               
            }
                
            item.ReduceCount();
            GameManager.ins.playerPressBuy = false;
            //Disable Card
            //DisableCard();
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
