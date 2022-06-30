using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerInput : MonoBehaviour
{
    private struct PlayerInputConstants
    {
        public const string Horizontal = "Horizontal";
        public const string Vertical = "Vertical";
        public const string Fire = "Fire1";
    }

    public Vector3 GetMovementInput()
    {
        float horizontalInput = Input.GetAxisRaw(PlayerInputConstants.Horizontal);

        if (Mathf.Approximately(horizontalInput, 0f))
        {
            horizontalInput = CrossPlatformInputManager.GetAxisRaw(PlayerInputConstants.Horizontal);
        }

        float verticalInput = Input.GetAxisRaw(PlayerInputConstants.Vertical);

        if (Mathf.Approximately(verticalInput, 0f))
        {
            verticalInput = CrossPlatformInputManager.GetAxisRaw(PlayerInputConstants.Vertical);
        }

        return new Vector3(horizontalInput, 0f, verticalInput);
    }

    public bool GetFireInput()
    {
        return CrossPlatformInputManager.GetButton(PlayerInputConstants.Fire);
    }
}
