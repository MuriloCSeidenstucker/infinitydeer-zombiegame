using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private AudioClip _deathClip;
    
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

    public bool PlayerActivated { get; private set; }

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
        if (!PlayerActivated) return;

        // North
        if (transform.position.z >= 37.64f)
        {
            transform.position = transform.position.WithAxis(Axis.Z, 37.64f);
        }

        // South
        if (transform.position.z <= -36.88)
        {
            transform.position = transform.position.WithAxis(Axis.Z, -36.88f);
        }

        // West
        if (transform.position.x <= -37.47f)
        {
            transform.position = transform.position.WithAxis(Axis.X, -37.47f);
        }

        // East
        if (transform.position.x >= 37.15f)
        {
            transform.position = transform.position.WithAxis(Axis.X, 37.15f);
        }

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
        Singleton.Instance.AudioService.PlayAudioCue(_deathClip);
        _playerMovement.StopImmediately();
        _collider.enabled = false;
        EnablePlayer(false);
        
        PlayerDeathEvent?.Invoke();
    }

    public void EnablePlayer(bool value) => PlayerActivated = value;

    private void OnDestroy()
    {
        if (_damageable == null) return;

        _damageable.DeathEvent -= OnDeath;
    }
}
