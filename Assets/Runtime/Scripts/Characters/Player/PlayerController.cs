using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterMovement _playerMovement;
    private PlayerInput _playerInput;
    private GunController _gunController;
    private AutoAim _autoAim;

    private void Awake()
    {
        _playerMovement = GetComponent<CharacterMovement>();
        _playerInput = GetComponent<PlayerInput>();
        _gunController = GetComponent<GunController>();
        _autoAim = GetComponent<AutoAim>();
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
}
