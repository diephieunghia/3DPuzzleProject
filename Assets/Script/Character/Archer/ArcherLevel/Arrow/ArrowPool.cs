using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class ArrowPool : ObjectPool
{
    [SerializeField] Transform arrowHolder;
    public static ArrowPool ins {  get; private set; }
    public int fireIce = -1;

    private void Awake()
    {
        if (ins != null && ins != this)
            Destroy(this);
        else
            ins = this;
    }

    protected override void Start()
    {
        //Setup pool of object based on count at the start of the game
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(prefab, arrowHolder);    

            obj.SetActive(false);
            
            poolObjects.Enqueue(obj);
        }
    }
    public override GameObject GetObject()
    {
        //return obj in queue
        if (poolObjects.Count > 0)
        {
            GameObject obj = poolObjects.Dequeue();
            obj.SetActive(true);
            obj.transform.localRotation = Quaternion.identity;
            obj.transform.localScale = Vector3.one;
            Arrow arrow = obj.GetComponent<Arrow>();
            if (fireIce == 1)
                arrow.FlipFireIce(true);
            else if (fireIce == 0)
                arrow.FlipFireIce(false);
            return obj;
        }
        //if queue is empty, instantiate new object
        else
        {   GameObject temp= Instantiate(prefab, arrowHolder);
            Arrow newArrow = temp.GetComponent<Arrow>();
            if (fireIce == 1)
                newArrow.FlipFireIce(true);
            else if (fireIce == 0)
                newArrow.FlipFireIce(false);
            return temp;
        }

    }
    public override void ReturnObject(GameObject obj)
    {        
        if(poolObjects.Count>45)
        {
            Destroy(obj);
            return;
        }
        obj.transform.SetParent(arrowHolder.transform, false);
        obj.transform.localRotation = Quaternion.identity;
        obj.transform.localScale = Vector3.one;
        obj.SetActive(false);
        poolObjects.Enqueue(obj);
    }    

}
