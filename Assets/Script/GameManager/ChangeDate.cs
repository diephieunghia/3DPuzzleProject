using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDate : MonoBehaviour
{
    public Material[] skyBox;
    public GameObject[] lightning;
    GameObject lightSet;
    public Color[] fog;
    public void ChangeSkyBox(int index)
    {       
        index= Mathf.Clamp(index, 0, skyBox.Length - 1);
        RenderSettings.skybox = skyBox[index];
        RenderSettings.fogColor = fog[index];
        if (lightSet!=null)
        {
            Destroy(lightSet);
        }
        lightSet = Instantiate(lightning[index], Vector3.zero, Quaternion.identity);

    }
}
