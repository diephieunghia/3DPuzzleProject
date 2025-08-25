using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadingText : MonoBehaviour
{
    string[] text= { " ."," . ."," . . ."};
    [SerializeField] TextMeshProUGUI loadingText;
    float cd = 0.5f;
    float temp = 0.5f;
    int index = 0;
    // Update is called once per frame
    void Update()
    {
        Debug.Log("running");
        temp-=Time.deltaTime;
        if (temp <= 0)
        {
            temp = cd;
            loadingText.text = string.Format("Loading{0}", text[index]);
            index++;
            if (index == 2) index = 0;

        }
        
    }
}
