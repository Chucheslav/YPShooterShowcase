using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class HumanoidController : MonoBehaviour
{
    [Min(0)] public float maxSpeedReference = 1f;
    public Vector3 moveVector;
    public float speedMultiplier = 1;
    
    public bool isAttacking => _animator.GetCurrentAnimatorStateInfo(0).IsName("Standing Melee Attack Horizontal");

    private CharacterController _characterController;
    private Animator _animator;
    private static readonly int Swing = Animator.StringToHash("Swing");
    private static readonly int Speed = Animator.StringToHash("Speed");

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if(!_animator) return;
        _animator.SetFloat(Speed, Mathf.Clamp(moveVector.magnitude/ maxSpeedReference, 0, 1));
    }

    private void FixedUpdate()
    {
        _characterController.Move(moveVector * speedMultiplier * Time.fixedDeltaTime);
    }

    public void ShowMelee()
    {
        _animator.SetLayerWeight(_animator.GetLayerIndex("PistolAim"), 0);
        _animator.SetTrigger(Swing);

        var t = _animator.GetCurrentAnimatorStateInfo(0);
    }
}
