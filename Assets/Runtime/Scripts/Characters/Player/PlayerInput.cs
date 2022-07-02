using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerInputActions _inputActions;

    private void Awake()
    {
        _inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        _inputActions.PlayerController.Enable();
    }

    public Vector3 GetMovementInput()
    {
        Vector2 rawInput = _inputActions.PlayerController.Movement.ReadValue<Vector2>();
        Vector3 processedInput = new Vector3(rawInput.x, 0f, rawInput.y);

        return processedInput;
    }

    public bool GetFireInput()
    {
        return _inputActions.PlayerController.Fire.IsPressed();
    }

    private void OnDisable()
    {
        _inputActions.PlayerController.Disable();
    }
}
