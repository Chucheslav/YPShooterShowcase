using UnityEngine;

[RequireComponent(typeof(HumanoidController))]
public class HCMoveFromAxis : MonoBehaviour
{
    private HumanoidController _controller;

    private void Awake()
    {
        _controller = GetComponent<HumanoidController>();
    }

    private void Update()
    {
        _controller.moveVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * 
                                 (Input.GetButton("Shift")? 1f: 0.4f);
    }
}