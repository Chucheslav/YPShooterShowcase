using UnityEngine;

public class CameraAssembly : MonoBehaviour
{
    [SerializeField] private Transform toFollow;
    [SerializeField] private Vector3 offset;
    [SerializeField] private bool followPosition;
    [SerializeField] private float minVerticalAngle;
    [SerializeField] private float maxVerticalAngle;
    [SerializeField] private Vector2 rotationSpeed;

    private float xAxisRotation;
    private float yAxisRotation;


    private void Update()
    {
        if(!toFollow) return;
        
        xAxisRotation += rotationSpeed.x * Input.GetAxis("Mouse Y") * Time.deltaTime;
        yAxisRotation += rotationSpeed.y * Input.GetAxis("Mouse X") * Time.deltaTime;
        xAxisRotation = Mathf.Clamp(xAxisRotation, minVerticalAngle, maxVerticalAngle);
        
        if (followPosition) transform.position = toFollow.position + offset;
        transform.eulerAngles = new Vector3(xAxisRotation, yAxisRotation, 0);
    }
}