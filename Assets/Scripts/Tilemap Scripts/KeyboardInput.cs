
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardInput : MonoBehaviour
{
    public Vector2 GetMovement()
    {
        if (IsKeyboardUnavailable())
        {
            return Vector2.zero;
        }

        float horizontalInput = GetHorizontalInput();
        float verticalInput = GetVerticalInput();

        Vector2 movement = new Vector2(horizontalInput, verticalInput);
        // Normalize diagonal movement so player doesn't move faster diagonally
        movement = AdjustForDiagonalMovement(movement);
        
        return movement;
    }

    public bool WasPickupButtonPressed()
    {
        if (IsKeyboardUnavailable())
        {
            return false;
        }
        
        return Keyboard.current[Key.E].wasPressedThisFrame;
    }

    public bool WasPlaceButtonPressed()
    {
        if (IsKeyboardUnavailable())
        {
            return false;
        }
        
        return Keyboard.current[Key.F].wasPressedThisFrame;
    }
    
    public bool WasAttackButtonPressed()
    {
        if (IsKeyboardUnavailable())
        {
            return false;
        }
        
        return Keyboard.current[Key.LeftShift].wasPressedThisFrame;
    }

    private bool IsKeyboardUnavailable()
    {
        return Keyboard.current == null;
    }

    // Returns -1 for left (A), +1 for right (D), or 0 for neither/both
    private float GetHorizontalInput()
    {
        float horizontal = 0f;
        
        if (IsLeftKeyPressed())
        {
            horizontal = horizontal - 1f;
        }
        
        if (IsRightKeyPressed())
        {
            horizontal = horizontal + 1f;
        }
        
        return horizontal;
    }

    // Returns -1 for down (S), +1 for up (W), or 0 for neither/both
    private float GetVerticalInput()
    {
        float vertical = 0f;
        
        if (IsUpKeyPressed())
        {
            vertical = vertical + 1f;
        }
        
        if (IsDownKeyPressed())
        {
            vertical = vertical - 1f;
        }
        
        return vertical;
    }

    private bool IsLeftKeyPressed()
    {
        return Keyboard.current.aKey.isPressed;
    }

    private bool IsRightKeyPressed()
    {
        return Keyboard.current.dKey.isPressed;
    }

    private bool IsUpKeyPressed()
    {
        return Keyboard.current.wKey.isPressed;
    }

    private bool IsDownKeyPressed()
    {
        return Keyboard.current.sKey.isPressed;
    }

    private Vector2 AdjustForDiagonalMovement(Vector2 movement)
    {
        if (IsDiagonalMovement(movement))
        {
            movement.Normalize();
        }
        
        return movement;
    }

    // sqrMagnitude > 1 means both X and Y have values (diagonal movement)
    private bool IsDiagonalMovement(Vector2 movement)
    {
        return movement.sqrMagnitude > 1f;
    }
}