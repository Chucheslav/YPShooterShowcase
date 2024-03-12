using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class HumanoidController : MonoBehaviour
{
    [Min(0)] public float maxSpeedReference = 1f;
    public Vector3 moveVector;
    public float speedMultiplier = 1;

    private CharacterController _characterController;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if(!_animator) return;
        _animator.SetFloat("Speed", Mathf.Clamp(moveVector.magnitude/ maxSpeedReference, 0, 1));
    }

    private void FixedUpdate()
    {
        _characterController.Move(moveVector * speedMultiplier * Time.fixedDeltaTime);
    }
}
