using UnityEngine;

public class Move2D : MonoBehaviour
{
    public float Speed = 6f;
    public Animator Animator;
    public SpriteRenderer SpriteRenderer;
    public KeyboardInput KeyboardInput;
    public GamepadInput GamepadInput;

    private Vector2 currentMovementInput;

    private void Awake()
    {
        InitializeComponents();
    }

    private void Update()
    {
        GetCombinedInput();
        UpdateAnimator();
        UpdateSpriteDirection();
        ApplyMovement();
    }
    
    // Auto-assigns component references if they weren't set in the Inspector
    private void InitializeComponents()
    {
        if (SpriteRenderer == null)
            SpriteRenderer = GetComponent<SpriteRenderer>();
        
        if (Animator == null)
            Animator = GetComponent<Animator>();
        
        if (KeyboardInput == null)
            KeyboardInput = GetComponent<KeyboardInput>();
        
        if (GamepadInput == null)
            GamepadInput = GetComponent<GamepadInput>();
    }

    // Gamepad input takes priority over keyboard when active
    private void GetCombinedInput()
    {
        Vector2 keyboardMovement = GetKeyboardMovement();
        Vector2 gamepadMovement = GetGamepadMovement();
        
        if (HasGamepadInput(gamepadMovement))
            currentMovementInput = gamepadMovement;
        else
            currentMovementInput = keyboardMovement;
    }

    private Vector2 GetKeyboardMovement()
    {
        if (KeyboardInput == null)
            return Vector2.zero;
        
        return KeyboardInput.GetMovement();
    }

    private Vector2 GetGamepadMovement()
    {
        if (GamepadInput == null)
            return Vector2.zero;
        
        return GamepadInput.GetMovement();
    }

    private bool HasGamepadInput(Vector2 gamepadMovement)
    {
        return gamepadMovement.sqrMagnitude > 0f;
    }

    private void UpdateAnimator()
    {
        if (Animator == null)
            return;
        
        bool isWalking = IsMoving();
        Animator.SetBool("IsWalking", isWalking);
    }

    private bool IsMoving()
    {
        return currentMovementInput.sqrMagnitude > 0f;
    }

    // Flips sprite horizontally based on movement direction (doesn't flip for vertical-only movement)
    private void UpdateSpriteDirection()
    {
        if (SpriteRenderer == null)
            return;

        if (IsMovingLeft())
            SpriteRenderer.flipX = true;
        else if (IsMovingRight())
            SpriteRenderer.flipX = false;
    }

    private bool IsMovingLeft()
    {
        return currentMovementInput.x < 0f;
    }

    private bool IsMovingRight()
    {
        return currentMovementInput.x > 0f;
    }

    // Directly modifies transform.position (not using physics)
    private void ApplyMovement()
    {
        Vector3 movementDelta = CalculateMovementDelta();
        transform.position = transform.position + movementDelta;
    }

    // Multiplies by Time.deltaTime for frame-rate independent movement
    private Vector3 CalculateMovementDelta()
    {
        return (Vector3)(currentMovementInput * Speed * Time.deltaTime);
    }
}