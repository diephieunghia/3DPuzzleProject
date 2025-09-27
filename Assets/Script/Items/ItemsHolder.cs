using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsHolder : MonoBehaviour
{
    public static ItemsHolder ins { get; private set; }
    public List<SO_Item> Items;
    public List<SO_Mons> mons;

    private void Awake()
    {
        if (ins != null && ins != this)
            Destroy(this);
        else
            ins = this;
        Debug.Log("Items holder inven: "+Items.Count);
        for (int i = 0; i < mons.Count; i++)
        {
           SO_Mons monster = ScriptableObject.Instantiate(mons[i]);
            Debug.Log(monster.name);
        }
        for (int i = 0; i < Items.Count; i++)
        {
            SO_Item item = ScriptableObject.Instantiate(Items[i]);
            Debug.Log(item.name);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
