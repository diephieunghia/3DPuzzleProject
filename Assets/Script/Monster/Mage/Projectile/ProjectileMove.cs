using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(FireBallDetection))]
public class ProjectileMove : MonoBehaviour
{
    Vector3 monsterDirection;
    float speed;
    public float Speed { get { return speed; } set { speed = value; } }
    public float SpeedTest;
    float damage;
    FireBallDetection fireballDetect;
    Transform[] child;

    void Start()
    {
        fireballDetect = GetComponent<FireBallDetection>();
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
        //pass damage to fireball detection
        fireballDetect.getDamage(damage);
    }

}
