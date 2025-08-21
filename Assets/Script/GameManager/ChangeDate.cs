using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDate : MonoBehaviour
{
    public Material[] skyBox;
    public GameObject[] lightning;
    GameObject light;
    public Color[] fog;
    public void ChangeSkyBox(int index)
    {       
        RenderSettings.skybox = skyBox[index];
        RenderSettings.fogColor = fog[index];
        if (light!=null)
        {
            Destroy(light);
        }
        light = Instantiate(lightning[index], Vector3.zero, Quaternion.identity);

    }
}
