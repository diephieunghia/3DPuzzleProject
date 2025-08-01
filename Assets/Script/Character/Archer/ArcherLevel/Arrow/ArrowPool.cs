using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPool : ObjectPool
{
    [SerializeField] Transform arrowHolder;
    public static ArrowPool ins {  get; private set; }


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
            obj.transform.localRotation= Quaternion.identity;
            obj.transform.localScale = Vector3.one;
            return obj;
        }
        //if queue is empty, instantiate new object
        else
            return Instantiate(prefab, arrowHolder);

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
