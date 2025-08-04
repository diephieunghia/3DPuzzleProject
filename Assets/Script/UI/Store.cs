using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    //interact store and open shop menu
    public GameObject eIcon;
    public GameObject rIcon; //r for refresh items

    bool enableAndDisable=false;

    private void Start()
    {
        GameManager.ins.CountDownComplete += ButtonEnDis;
    }

    private void ButtonEnDis()
    {
        StartCoroutine(WaitAndActi());
    }
    IEnumerator WaitAndActi()
    {
        yield return new WaitForSeconds(0.75f);
        enableAndDisable = !enableAndDisable;
        eIcon.SetActive(enableAndDisable);
        rIcon.SetActive(enableAndDisable);
    }
}
