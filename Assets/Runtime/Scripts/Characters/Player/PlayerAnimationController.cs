using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private PlayerController _player;
    private Animator _animator;

    private const string c_velocity = "Velocity";
    private const string c_isDead = "IsDead";

    private void Awake()
    {
        _player = GetComponentInParent<PlayerController>();
        _animator = GetComponent<Animator>();

        _player.PlayerDeathEvent += OnPlayerDeath;
    }

    private void LateUpdate()
    {
        _animator.SetFloat(c_velocity, _player.CurrentVelocity.sqrMagnitude);
    }

    private void OnPlayerDeath()
    {
        _animator.SetTrigger(c_isDead);
    }
}
