using UnityEngine;

[RequireComponent(typeof(HumanoidController))]
public class HcAttackFromInput : MonoBehaviour
{
    private HumanoidController _controller;

    private void Awake()
    {
        _controller = GetComponent<HumanoidController>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire2")) _controller.ShowMelee();
    }
}