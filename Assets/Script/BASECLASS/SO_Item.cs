using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum Rarity
{
    Common,
    Rare,
    Epic,
    Legend
}
public enum ItemType
{
    Stat,
    Special,
    Skill,
    Passive
}
[CreateAssetMenu(fileName = "SO", menuName = "ScriptableObjects/Items")]

public class SO_Item : ScriptableObject
{
    [Header("Description")]
    //Description
    public string text;
    public Sprite icon;
    //Cost
    public float cost;
    //quantity for spawn this
    public int quantity;
    //type of buff
    [Header("Item Type Rarity")]
    public ItemType type;
    public Rarity rarity;
    public Color textColor = Color.white;

    [Header("Stat")]
    public float health;
    public float armor;
    public float skillCD;
    public float dashCoolDown;

    //special name
    public string upgradeType;
    //damage
    public float damage;
    public float maxDamage;
    public int arrowCount;

    public float speed;
    public float tempSpeed;
    //e
    public float eDuration;
    public float eCoolDown;
    public float animSpeed;

    //q
    public float qDuration;
    public float qCoolDown;
    public float qanimSpeed;

    [Header("Object Spawn")]
    //Object to Spawn
    public TerrainCollider[] terrain;
    public GameObject summon;
    public bool isVisible = false;

    //Spawnable Item multiplier
    public float passiveCD;
    public float passiveDMG;
    public float passiveArea;

    TerrainData[] tData;
    Vector3[] terrainPos;
    public void SpawnObject(bool visible,int random,ArcherBlackBoard bb)
    {
        if (visible)
        {
            tData=new TerrainData[terrain.Length];
            terrainPos=new Vector3[terrain.Length];
            for (int i = 0; i < terrain.Length; i++)
            {
                tData[i] = terrain[i].terrainData;
                terrainPos[i] = terrain[i].transform.position;
            }
            //random x and z inside terrain size
            int k = Random.Range(0, terrain.Length);
            float x = Random.Range(0f, tData[random].size.x);
            float z = Random.Range(0f, tData[random].size.z);

            float y = tData[random].GetHeight(Mathf.RoundToInt(x), Mathf
               .RoundToInt(z));
            //Convert local coords to world coords
            Vector3 spawnPos = new Vector3(
                x + terrainPos[random].x,
                y + terrainPos[random].y ,
                z + terrainPos[random].z);
            Debug.Log("Spawn " + summon.name + " at " + spawnPos);
            Instantiate(summon, spawnPos, Quaternion.identity);
        }
        else//just spawn for script to work
        {
            Instantiate(summon, Vector3.zero, Quaternion.identity);
            summon.GetComponent<ItemGetData>()?.GetCharStatAtStart(bb);
        }
    }
    public void ReduceCount()
    {
        quantity -= 1;
    }
    public int RandomInt()
    {
        return Random.Range(0, terrain.Length);
    }


#if UNITY_EDITOR
    private void OnValidate()
    {
        switch (rarity)
        {
            case Rarity.Common:
                textColor = Color.white;
                break;        
            case Rarity.Rare:
                textColor = Color.blue;
                break;
            case Rarity.Epic:
                textColor = new Color(0.64f, 0.21f, 0.93f); // purple
                break;
            case Rarity.Legend:
                textColor = new Color(1f, 0.5f, 0f); // orange
                break;
        }
    }
#endif

}
