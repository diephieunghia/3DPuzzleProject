using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    //test using public
    List<SO_Item> items;
    public List<SO_Item> Items => items;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.ins.CountDownComplete += AssignCardUI;
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
        int max=Mathf.Min(3,temp.Length);
        for (int i = 0; i < max; i++)
        {
            sprites[i].sprite = items[i].icon;
            itemNames[i].text = items[i].name;
            descriptions[i].text = items[i].text;
            itemHolder[i].GetComponent<CardChooseAndIncreaseStat>()?.AssignItem(items[i]);
            value[i].text=items[i].cost.ToString();
        }            
    }
}
