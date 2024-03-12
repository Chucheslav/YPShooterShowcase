﻿using UnityEngine;

[RequireComponent(typeof(HumanoidController))]
public class HCMoveFromAxisAnCameraDirection : MonoBehaviour
{
    [SerializeField] private TransformVariable cameraAssemblyTransform;
    
    private HumanoidController _controller;

    private void Awake()
    {
        _controller = GetComponent<HumanoidController>();
    }

    private void Update()
    {
        if(!cameraAssemblyTransform || !cameraAssemblyTransform.Value) return;
        Vector3 inputVector = new Vector3(Input.GetAxis("Vertical"),0, Input.GetAxis("Horizontal")).normalized;

        Vector3 moveVector = Vector3.zero;
        if (inputVector.magnitude > 0.01f)
        {
            Vector3 cameraForward = cameraAssemblyTransform.Value.forward;
            Vector3 upRef = transform.up;
            Vector3.OrthoNormalize(ref upRef, ref cameraForward);

            transform.rotation = Quaternion.LookRotation(cameraForward, transform.up);
        
            moveVector = (cameraForward * Input.GetAxis("Vertical") + cameraAssemblyTransform.Value.right * Input.GetAxis("Horizontal")).normalized;
            transform.rotation = Quaternion.LookRotation(moveVector, transform.up);
        } 
        
        _controller.moveVector = moveVector;
    }
}