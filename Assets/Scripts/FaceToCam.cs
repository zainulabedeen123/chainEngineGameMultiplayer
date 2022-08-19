using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceToCam : MonoBehaviour
{

    Camera cam; 
 

    //public Transform camTransform;

    //.Quaternion originalRotation;

    void Start()
    {
        GetComponent<Canvas>().worldCamera = FindObjectOfType<Camera>();
        // originalRotation = transform.rotation;
    }

    void Update()
    {
        if (cam == null)
        {
            cam = FindObjectOfType<Camera>();

        }
        if (cam == null)
        {
            return;

        }

        transform.LookAt(cam.transform);
        transform.Rotate(Vector3.up * 180);
        //transform.rotation = camTransform.rotation * originalRotation;
    }
}