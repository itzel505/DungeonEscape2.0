using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardInput : MonoBehaviour
{
    public Vector2 GetMovement()
    {
        if (Keyboard.current == null)
            return Vector2.zero;

        float horizontal = GetHorizontalInput();
        float vertical = GetVerticalInput();

        Vector2 movement = new Vector2(horizontal, vertical);
        return NormalizeIfDiagonal(movement);
    }

    public bool WasPickupButtonPressed()
    {
        return Keyboard.current != null && Keyboard.current[Key.E].wasPressedThisFrame;
    }

    public bool WasPlaceButtonPressed()
    {
        return Keyboard.current != null && Keyboard.current[Key.F].wasPressedThisFrame;
    }

    public bool WasAttackButtonPressed()
    {
        return Keyboard.current != null && Keyboard.current[Key.LeftShift].wasPressedThisFrame;
    }

    private float GetHorizontalInput()
    {
        float horizontal = 0f;

        if (Keyboard.current.aKey.isPressed)
            horizontal -= 1f;

        if (Keyboard.current.dKey.isPressed)
            horizontal += 1f;

        return horizontal;
    }

    private float GetVerticalInput()
    {
        float vertical = 0f;

        if (Keyboard.current.wKey.isPressed)
            vertical += 1f;

        if (Keyboard.current.sKey.isPressed)
            vertical -= 1f;

        return vertical;
    }

    private Vector2 NormalizeIfDiagonal(Vector2 movement)
    {
        if (movement.sqrMagnitude > 1f)
            movement.Normalize();

        return movement;
    }
}