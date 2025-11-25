using UnityEngine;
using UnityEngine.InputSystem;

public class GamepadInput : MonoBehaviour
{
    // Minimum stick movement required to register input (prevents stick drift)
    public float Deadzone = 0.2f;
    // Which gamepad to read from when multiple are connected (0 = first gamepad)
    public int GamepadIndex = 0;
    // If true, D-pad overrides analog stick when pressed
    public bool UseDpad = true;

    public Vector2 GetMovement()
    {
        Gamepad gamepad = GetGamepad();
        
        if (gamepad == null)
        {
            return Vector2.zero;
        }

        Vector2 movement = GetStickMovement(gamepad);
        
        // D-pad takes priority over analog stick if it's being used
        if (UseDpad)
        {
            movement = GetDpadMovementIfActive(gamepad, movement);
        }
        
        // Normalize diagonal movement so player doesn't move faster diagonally
        movement = AdjustForDiagonalMovement(movement);
        
        return movement;
    }

    // buttonSouth = A on Xbox, X on PlayStation
    public bool WasPickupButtonPressed()
    {
        if (Gamepad.current == null)
        {
            return false;
        }
        
        return Gamepad.current.buttonSouth.wasPressedThisFrame;
    }

    // buttonNorth = Y on Xbox, Triangle on PlayStation
    public bool WasPlaceButtonPressed()
    {
        if (Gamepad.current == null)
        {
            return false;
        }
        
        return Gamepad.current.buttonNorth.wasPressedThisFrame;
    }
    
    // buttonWest = X on Xbox, Square on PlayStation
    public bool WasAttackButtonPressed()
    {
        if (Gamepad.current == null)
        {
            return false;
        }
        
        return Gamepad.current.buttonWest.wasPressedThisFrame;
    }

    private Gamepad GetGamepad()
    {
        if (HasNoGamepads())
        {
            return null;
        }
        
        if (IsInvalidGamepadIndex())
        {
            return null;
        }
        
        return Gamepad.all[GamepadIndex];
    }

    private bool HasNoGamepads()
    {
        return Gamepad.all.Count == 0;
    }

    private bool IsInvalidGamepadIndex()
    {
        return GamepadIndex < 0 || GamepadIndex >= Gamepad.all.Count;
    }

    private Vector2 GetStickMovement(Gamepad gamepad)
    {
        Vector2 stickInput = gamepad.leftStick.ReadValue();
        
        // Ignore input below deadzone threshold to prevent unintended movement
        if (IsStickInputAboveDeadzone(stickInput))
        {
            return stickInput;
        }
        
        return Vector2.zero;
    }

    private bool IsStickInputAboveDeadzone(Vector2 stickInput)
    {
        return stickInput.magnitude >= Deadzone;
    }

    // Returns D-pad input if active, otherwise returns the current movement (analog stick)
    private Vector2 GetDpadMovementIfActive(Gamepad gamepad, Vector2 currentMovement)
    {
        Vector2 dpadInput = gamepad.dpad.ReadValue();
        
        if (IsDpadActive(dpadInput))
        {
            return dpadInput;
        }
        
        return currentMovement;
    }

    private bool IsDpadActive(Vector2 dpadInput)
    {
        return dpadInput.sqrMagnitude > 0f;
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