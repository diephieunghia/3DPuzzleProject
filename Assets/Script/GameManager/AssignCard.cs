using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AssignCard : MonoBehaviour
{
    public Image[] sprites;
    public Image[] rarityColor;
    public TextMeshProUGUI[] itemNames;
    public TextMeshProUGUI[] descriptions;
    public GameObject[] itemHolder;

    //test using public
    public SO_Item[] items;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.ins.CountDownComplete += AssignCardUI;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void AssignCardUI()
    {
        items = Resources.LoadAll<SO_Item>("Items");
        for (int i = 0; i < items.Length; i++)
        {
            sprites[i].sprite = items[i].icon;
            itemNames[i].text = items[i].name;
            descriptions[i].text = items[i].text;
            itemHolder[i].GetComponent<StoreUI>()?.AssignItem(items[i]);
        }
            

    }
}
