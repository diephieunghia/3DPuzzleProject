using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterPool : ObjectPool
{
    public Transform navMeshLocation;
    // Start is called before the first frame update
    protected override void Start()
    {      
        Vector3 location = HitLocation();
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(prefab, transform);
            obj.transform.position = location;
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
            if (obj != null)
            {
                obj.SetActive(true);
                return obj;
            }
            else return null;
        }
        //if queue is empty, instantiate new object
        else
            return Instantiate(prefab, transform);
    }
    Vector3 HitLocation()
    {
        Vector3 location = Vector3.zero;
        if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 4f, NavMesh.AllAreas))
            location = hit.position;        
        return location;
    }


}
