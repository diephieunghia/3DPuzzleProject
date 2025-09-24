using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class AssignCard : MonoBehaviour
{
    //in game manager
    public Image[] sprites;
    public Image[] rarityColor;
    public TextMeshProUGUI[] itemNames;
    public TextMeshProUGUI[] descriptions;
    public GameObject[] itemHolder;
    public TextMeshProUGUI[] value;

    public GameObject[] card;
    //test using public
    List<SO_Item> items;
    public List<SO_Item> Items => items;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.ins.CountDownComplete += AssignCardUI;
        items = new List<SO_Item>();
    }

    void AssignCardUI()
    {
        SO_Item[] temp;
        temp = Resources.LoadAll<SO_Item>("Items");
        //assign items to list
        foreach (SO_Item item in temp)
        {
            items.Add(item);
        }
        SetToCard(temp);
    }
    public void ReAssign()
    {
        SO_Item[] temp = RandomizeItem();
        //card enable
        for (int i = 0; i < itemHolder.Length; i++)
        {
            itemHolder[i].SetActive(true);
            card[i].SetActive(true);
            
        }
        SetToCard(temp);
    }
    void SetToCard(SO_Item[] items)
    {
        int max = Mathf.Min(3, items.Length);
        for (int i = 0; i < max; i++)
        {
            sprites[i].sprite = items[i].icon;
            itemNames[i].text = items[i].name;
            descriptions[i].text = items[i].text;
            itemHolder[i].GetComponent<CardChooseAndIncreaseStat>()?.AssignItem(items[i]);
            value[i].text = items[i].cost.ToString();
        }
    }
    public bool CheckItemAmountRemoveFromList(SO_Item item)
    {

        if (item.quantity == 0)
        { 
            items.Remove(item);
            return true;
        }
        return false;
    }
    SO_Item[] RandomizeItem()
    {
        SO_Item[] temp = new SO_Item[3];
        for (int i = 0; i < 3; i++)
        {
            int rand = Random.Range(0, items.Count);
            temp[i] = items[rand];
        }
        return temp;
    }
    
}
