using UnityEngine;
using UnityEngine.InputSystem;

public class GamepadInput : MonoBehaviour
{
    [Tooltip("Minimum stick movement required to register input (prevents stick drift)")]
    public float Deadzone = 0.2f;
    
    [Tooltip("Which gamepad to read from when multiple are connected (0 = first gamepad)")]
    public int GamepadIndex = 0;
    
    [Tooltip("If true, D-pad overrides analog stick when pressed")]
    public bool UseDpad = true;

    public Vector2 GetMovement()
    {
        Gamepad gamepad = GetGamepad();
        if (gamepad == null)
            return Vector2.zero;

        Vector2 movement = GetStickMovement(gamepad);

        if (UseDpad)
            movement = GetDpadMovementIfActive(gamepad, movement);

        return NormalizeIfDiagonal(movement);
    }

    public bool WasPickupButtonPressed()
    {
        return Gamepad.current != null && Gamepad.current.buttonSouth.wasPressedThisFrame;
    }

    public bool WasPlaceButtonPressed()
    {
        return Gamepad.current != null && Gamepad.current.buttonNorth.wasPressedThisFrame;
    }

    public bool WasAttackButtonPressed()
    {
        return Gamepad.current != null && Gamepad.current.buttonWest.wasPressedThisFrame;
    }

    private Gamepad GetGamepad()
    {
        if (Gamepad.all.Count == 0)
            return null;

        if (GamepadIndex < 0 || GamepadIndex >= Gamepad.all.Count)
            return null;

        return Gamepad.all[GamepadIndex];
    }

    private Vector2 GetStickMovement(Gamepad gamepad)
    {
        Vector2 stickInput = gamepad.leftStick.ReadValue();
        return stickInput.magnitude >= Deadzone ? stickInput : Vector2.zero;
    }

    private Vector2 GetDpadMovementIfActive(Gamepad gamepad, Vector2 currentMovement)
    {
        Vector2 dpadInput = gamepad.dpad.ReadValue();
        return dpadInput.sqrMagnitude > 0f ? dpadInput : currentMovement;
    }

    private Vector2 NormalizeIfDiagonal(Vector2 movement)
    {
        if (movement.sqrMagnitude > 1f)
            movement.Normalize();

        return movement;
    }
}