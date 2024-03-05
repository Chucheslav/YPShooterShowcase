using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public Transform CameraAxisTransform;
    public float minAngle;
    public float maxAngle;

    public float RotationsSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y + Time.deltaTime * RotationsSpeed * Input.GetAxis("Mouse X"), 0);
        var nevAngleX = CameraAxisTransform.localEulerAngles.x - Time.deltaTime * RotationsSpeed * Input.GetAxis("Mouse Y");
        if (nevAngleX > 180)
            nevAngleX -= 360;

        nevAngleX = Mathf.Clamp(nevAngleX, minAngle, maxAngle);

        CameraAxisTransform.localEulerAngles = new Vector3(nevAngleX,0, 0);
    }
}
