using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AssignCard : MonoBehaviour
{
    public Image[] sprites;
    public TextMeshProUGUI[] itemNames;
    public TextMeshProUGUI[] descriptions;

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
        //still not assgin
        items = Resources.LoadAll<SO_Item>("Assets/ScriptableObjects/Items");
        Debug.Log("Assigned: " + items.Length);
    }
}
