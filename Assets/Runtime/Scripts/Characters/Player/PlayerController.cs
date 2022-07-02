using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterMovement _playerMovement;
    private PlayerInput _playerInput;
    private GunController _gunController;
    private AutoAim _autoAim;
    private HandleDamage _handleDamage;
    private Collider _collider;
    private IDamageable _damageable;

    public Vector3 CurrentVelocity => _playerMovement.CurrentVelocity;
    public int Health => _handleDamage.Health;
    public int DefaultHealth => _handleDamage.DefaultHealth;

    public event Action PlayerDeathEvent;

    private void Awake()
    {
        _playerMovement = GetComponent<CharacterMovement>();
        _playerInput = GetComponent<PlayerInput>();
        _gunController = GetComponent<GunController>();
        _autoAim = GetComponent<AutoAim>();
        _handleDamage = GetComponent<HandleDamage>();
        _collider = GetComponent<Collider>();

        _damageable = GetComponent<IDamageable>();
        _damageable.DeathEvent += OnDeath;
    }

    private void Update()
    {
        bool fireInput = _playerInput.GetFireInput();
        Vector3 movementInput = _playerInput.GetMovementInput();
        Vector3 rotationInput = _autoAim.LookAtTarget(fireInput);

        _playerMovement.ProcessMovementInput(movementInput);
        _playerMovement.ProcessRotationInput(rotationInput);

        if (fireInput)
        {
            _gunController.Shoot();
        }
    }

    private void OnDeath()
    {
        _playerMovement.StopImmediately();
        _collider.enabled = false;
        enabled = false;
        
        PlayerDeathEvent?.Invoke();
    }

    private void OnDestroy()
    {
        if (_damageable == null) return;

        _damageable.DeathEvent -= OnDeath;
    }
}
