using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private bool _activateMouseInput = false;
    
    private CharacterMovement _playerMovement;
    private Vector3 _worldMousePos;

    private struct PlayerInputConstants
    {
        public const string Horizontal = "Horizontal";
        public const string HorizontalMovement = "HorizontalMovement";
        public const string HorizontalRotation = "HorizontalRotation";
        public const string Vertical = "Vertical";
        public const string VerticalMovement = "VerticalMovement";
        public const string VerticalRotation = "VerticalRotation";

    }

    private void Awake()
    {
        _playerMovement = GetComponent<CharacterMovement>();

        _activateMouseInput = false;
    }

    private void Update()
    {
        Vector3 movementInput = GetMovementInput();
        Vector3 rotationInput = GetRotationInput();

        _playerMovement.ProcessMovementInput(movementInput);
        _playerMovement.ProcessRotationInput(rotationInput);
    }

    private void FixedUpdate()
    {
        _worldMousePos = GetWorldMousePosition();
    }

    private Vector3 GetMovementInput()
    {
        float horizontalInput = Input.GetAxisRaw(PlayerInputConstants.Horizontal);

        if (Mathf.Approximately(horizontalInput, 0f))
        {
            horizontalInput = CrossPlatformInputManager.GetAxisRaw(PlayerInputConstants.HorizontalMovement);
        }

        float verticalInput = Input.GetAxisRaw(PlayerInputConstants.Vertical);

        if (Mathf.Approximately(verticalInput, 0f))
        {
            verticalInput = CrossPlatformInputManager.GetAxisRaw(PlayerInputConstants.VerticalMovement);
        }

        return new Vector3(horizontalInput, 0f, verticalInput);
    }

    private Vector3 GetRotationInput()
    {
        Vector3 direction = Vector3.zero;

        if (_activateMouseInput)
        {
            direction = _worldMousePos - transform.position;
        }
        else
        {
            direction.x = CrossPlatformInputManager.GetAxisRaw(PlayerInputConstants.HorizontalRotation);
            direction.z = CrossPlatformInputManager.GetAxisRaw(PlayerInputConstants.VerticalRotation);
        }

        return direction;
    }

    private Vector3 GetWorldMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float maxDist = 1000.0f;
        Vector3 mousePos = Vector3.zero;

        if (Physics.Raycast(ray, out var hitData, maxDist))
        {
            mousePos = hitData.point;
        }

        return mousePos;
    }
}
