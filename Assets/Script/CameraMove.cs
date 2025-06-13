using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform CharacterCameraHolder;

    // Update is called once per frame
    void Update()
    {
        transform.position = CharacterCameraHolder.transform.position;

    }
}
