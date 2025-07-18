using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
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
        
        for(int i=1;i< child.Length; i++)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            child[i].rotation = lookRotation* Quaternion.Euler(-90, 0, 0);
            
        }   
        
    }

}
