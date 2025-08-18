using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float mouseSensivity = 100f;
    public Transform meshRotate;

    float xRotation = 0f;
    float yRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * GameSettings.ins.mouseSensivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * GameSettings.ins.mouseSensivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 76f);

        yRotation += mouseX;
        //yRotation = Mathf.Clamp(yRotation, -88f, 88f);

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        meshRotate.rotation= Quaternion.Euler(0, yRotation, 0);        
    }
}
