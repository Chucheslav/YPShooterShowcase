using System;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
       //transform.position += transform.forward * Input.GetAxis("Mouse ScrollWheel");
       _camera.fieldOfView += Input.GetAxis("Mouse ScrollWheel");
       
    }
}
