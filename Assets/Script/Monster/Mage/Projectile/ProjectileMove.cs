using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    Collider[] hitColliders=null;

    Vector3 monsterDirection;
    float speed;
    public float Speed { get { return speed; } set { speed = value; } }
    public float SpeedTest;
    float damage;
    public float Damage => damage;

    Transform[] child;

    void Start()
    {
    }

    void Update()
    {
        
        transform.Translate(monsterDirection*Time.deltaTime*speed);
    }

    public void GetDirection(Vector3 direction, Quaternion rotation, float damage)
    {
        monsterDirection = direction;
        this.damage=damage;
        speed = SpeedTest;

        child = gameObject.GetComponentsInChildren<Transform>();
        Debug.Log(child.Length);
        //foreach(Transform t in child)
        //{
        //    t.rotation = rotation;
        //    t.rotation = Quaternion.Euler(new Vector3(-90, 0, 0));
        //}
    }

}
