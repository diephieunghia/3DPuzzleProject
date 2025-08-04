using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreUI : MonoBehaviour
{
    //Image for highlight
    public Image image;

    public Color highlight;
    public Color normal;
    
    private void OnTriggerEnter(Collider other)
    {
        image.color= highlight;
        Debug.Log(other.name);
    }
    private void OnTriggerExit(Collider other)
    {
        image.color = normal;
    }
}
