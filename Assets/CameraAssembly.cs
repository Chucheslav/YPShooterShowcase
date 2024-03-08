using System;
using UnityEngine;

public class CameraAssembly : MonoBehaviour
{
    [SerializeField] private TransformVariable toFollow;
    [SerializeField] private Vector3 offset;
    [SerializeField] private bool followPosition;
    [SerializeField] private float minVerticalAngle;
    [SerializeField] private float maxVerticalAngle;
    [SerializeField] private Vector2 rotationSpeed;

    private float xAxisRotation;
    private float yAxisRotation;

    private void Update()
    {
        xAxisRotation += rotationSpeed.x * Input.GetAxis("Mouse Y") * Time.deltaTime;
        yAxisRotation += rotationSpeed.y * Input.GetAxis("Mouse X") * Time.deltaTime;
        xAxisRotation = Mathf.Clamp(xAxisRotation, minVerticalAngle, maxVerticalAngle);
        transform.eulerAngles = new Vector3(xAxisRotation, yAxisRotation, 0);
        
        if(!toFollow.Value) return;
        if (followPosition) transform.position = toFollow.Value.position + offset;
    }
}