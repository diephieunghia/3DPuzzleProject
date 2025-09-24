using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public GameObject[] card;
    //test using public
    List<SO_Item> items;
    public List<SO_Item> Items => items;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.ins.CountDownComplete += AssignCardUI;
        items = new List<SO_Item>();
        SO_Item[] temp;
        temp = Resources.LoadAll<SO_Item>("Assets/Data/Items");
        Debug.Log("temp items " + temp.Length);
        //assign items to list
        foreach (SO_Item item in temp)
        {
            SO_Item instant = ScriptableObject.Instantiate(item);
            instant.name = item.name;
            items.Add(instant);

        }
    }

    void AssignCardUI()
    {
        
        SetToCard(items);
    }
    public void ReAssign()
    {
        List<SO_Item> temp = RandomizeItem();
        //card enable
        for (int i = 0; i < itemHolder.Length; i++)
        {
            itemHolder[i].GetComponent<BoxCollider>().enabled = true;
            card[i].SetActive(true);
            
        }
        SetToCard(temp);
    }
    void SetToCard(List<SO_Item> _items)
    {
        int max = Mathf.Min(3, _items.Count);
        for (int i = 0; i < max; i++)
        {
            sprites[i].sprite = _items[i].icon;
            itemNames[i].text = _items[i].name;
            descriptions[i].text = _items[i].text;
            itemHolder[i].GetComponent<CardChooseAndIncreaseStat>()?.AssignItem(_items[i]);
            value[i].text = _items[i].cost.ToString();
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
    List<SO_Item> RandomizeItem()
    {
        List<SO_Item> temp = new List<SO_Item> ();
        for (int i = 0; i < 3; i++)
        {
            int rand = Random.Range(0, items.Count);
            temp.Add(items[rand]);
        }
        return temp;
    }
    
}
