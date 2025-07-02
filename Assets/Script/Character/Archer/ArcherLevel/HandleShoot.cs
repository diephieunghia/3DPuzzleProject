using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
[RequireComponent(typeof(ArcherAction))]
public class HandleShoot : MonoBehaviour
{
    ArcherAction archerAction;
    [SerializeField] GameObject arrow;

    bool coroutineStart = false;
  
    // Start is called before the first frame update
    void Start()
    {
        archerAction = GetComponent<ArcherAction>();
        archerAction.bb.DrawArrow += HandleDrawArrow;
        
    }
    private void Update()
    {
        
    }
    void HandleDrawArrow(ArcherBlackBoard.Aim type)
    {
        if (type == ArcherBlackBoard.Aim.Hold)
        {
            //StartCoroutine("DecreaseSpeedAim",archerAction.bb.speed);

            StartCoroutine("IncreaseForce",archerAction.bb.force);
            coroutineStart = true;
        }
        else if (type == ArcherBlackBoard.Aim.Cancel)
        {
            StopAllCoroutines();
            if (coroutineStart) coroutineStart = false;

        }
        else if(type==ArcherBlackBoard.Aim.Shoot)
        {
            ShootArrow();
            StopAllCoroutines();
            if (coroutineStart) coroutineStart = false;
        }
       
    }
    IEnumerator DecreaseSpeedAim(float speed)
    {
        while (archerAction.bb.speed >= archerAction.bb.tempSpeed / 5f)
        {
            archerAction.bb.speed -= 0.08f;
            yield return new WaitForSeconds(.01f);           
        }       
        yield return null;       
    }
    IEnumerator IncreaseForce(float force)
    {
        while (archerAction.bb.force <= archerAction.bb.maxForce)
        {
            archerAction.bb.force += 2f;
            yield return new WaitForSeconds(.01f);
        }
        yield return null;
    }
    void ShootArrow()
    {

    }
}
