using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private CharacterMovement _playerMovement;
    private Animator _animator;

    private const string c_velocity = "Velocity";

    private void Awake()
    {
        _playerMovement = GetComponentInParent<CharacterMovement>();
        _animator = GetComponent<Animator>();
    }

    private void LateUpdate()
    {
        _animator.SetFloat(c_velocity, _playerMovement.CurrentVelocity.sqrMagnitude);
    }
}
