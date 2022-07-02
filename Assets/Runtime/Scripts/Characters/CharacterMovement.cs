using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Quaternion _currentRotation;
    private Vector3 _currentVelocity;
    Vector3 _rotationInLastFrame;

    public Vector3 CurrentVelocity { get { return _currentVelocity; } }

    [field: SerializeField]
    public CharacterParameters MovementParameters { get; set; }

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

    private Vector3 SetDefaultRotation()
    {
        if (_currentVelocity == Vector3.zero) return _rotationInLastFrame;

        _rotationInLastFrame = _currentVelocity;
        return _currentVelocity;
    }

    public void ProcessMovementInput(Vector3 movementInput)
    {
        Vector3 desiredVelocity = movementInput.normalized * MovementParameters.MovementSpeed;
        _currentVelocity = Vector3.MoveTowards(_currentVelocity, desiredVelocity, MovementParameters.MovementAcc * Time.deltaTime);
    }

    public void ProcessRotationInput(Vector3 rotationInput)
    {
        if (rotationInput == Vector3.zero)
        {
            rotationInput = SetDefaultRotation();
        }
        else
        {
            _rotationInLastFrame = rotationInput;
        }

        Vector3 correctedRotationInput = new Vector3(rotationInput.x, 0f, rotationInput.z);
        Quaternion desiredRotation = Quaternion.LookRotation(correctedRotationInput);
        _currentRotation = Quaternion.Slerp(_currentRotation, desiredRotation, MovementParameters.RotationAcc * Time.deltaTime);
    }

    public void StopImmediately()
    {
        _currentVelocity = Vector3.zero;
    }
}
