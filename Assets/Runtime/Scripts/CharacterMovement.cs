using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 10.0f;
    [SerializeField] private float _movementAcc = 100.0f;
    [SerializeField] private float _rotationAcc = 100.0f;

    private Rigidbody _rigidbody;
    private Quaternion _currentRotation;
    private Vector3 _currentVelocity;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        MoveCharacter();
        RotateCharacter();
    }

    private void MoveCharacter()
    {
        Vector3 previousPosition = _rigidbody.position;
        Vector3 currentPosition = previousPosition + _currentVelocity * Time.fixedDeltaTime;
        _rigidbody.MovePosition(currentPosition);
    }

    private void RotateCharacter()
    {
        _rigidbody.MoveRotation(_currentRotation.normalized);
    }

    public void ProcessMovementInput(Vector3 movementInput)
    {
        Vector3 desiredVelocity = movementInput * _movementSpeed;
        _currentVelocity = Vector3.MoveTowards(_currentVelocity, desiredVelocity, _movementAcc * Time.deltaTime);
    }

    public void ProcessRotationInput(Vector3 rotationInput)
    {
        if (rotationInput == Vector3.zero) return;
        
        Vector3 correctedRotationInput = new Vector3(rotationInput.x, 0f, rotationInput.z);
        Quaternion desiredRotation = Quaternion.LookRotation(correctedRotationInput);
        _currentRotation = Quaternion.Slerp(_currentRotation, desiredRotation, _rotationAcc * Time.deltaTime);
    }
}
