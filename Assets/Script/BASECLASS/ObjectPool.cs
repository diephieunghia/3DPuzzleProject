using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public int count;
    public Queue<GameObject> poolObjects = new Queue<GameObject>();
    public GameObject prefab;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        //Setup pool of object based on count at the start of the game
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(prefab, transform);
            obj.SetActive(false);
            poolObjects.Enqueue(obj);
        }
    }

    public virtual GameObject GetObject()
    {
        //return obj in queue
        if (poolObjects.Count > 0)
        {
            GameObject obj = poolObjects.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        //if queue is empty, instantiate new object
        else
            return Instantiate(prefab, transform);
    }

    public virtual void ReturnObject(GameObject obj)
    {
        if (poolObjects.Count >= 20)
        {
            Destroy(obj);
            return;
        }     
        obj.SetActive(false);
        poolObjects.Enqueue(obj);
    }

}
