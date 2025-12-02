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

    private void GetCombinedInput()
    {
        Vector2 gamepadMovement = GamepadInput != null ? GamepadInput.GetMovement() : Vector2.zero;
        Vector2 keyboardMovement = KeyboardInput != null ? KeyboardInput.GetMovement() : Vector2.zero;

        // Gamepad takes priority when active
        currentMovementInput = gamepadMovement.sqrMagnitude > 0f ? gamepadMovement : keyboardMovement;
    }

    private void UpdateAnimator()
    {
        if (Animator == null)
            return;

        bool isWalking = currentMovementInput.sqrMagnitude > 0f;
        Animator.SetBool("IsWalking", isWalking);
    }

    private void UpdateSpriteDirection()
    {
        if (SpriteRenderer == null)
            return;

        if (currentMovementInput.x < 0f)
            SpriteRenderer.flipX = true;
        else if (currentMovementInput.x > 0f)
            SpriteRenderer.flipX = false;
    }

    private void ApplyMovement()
    {
<<<<<<< Updated upstream
        Vector3 movementDelta = CalculateMovementDelta();
        transform.position = transform.position + movementDelta;
    }

    // Multiplies by Time.deltaTime for frame-rate independent movement
    private Vector3 CalculateMovementDelta()
    {
        return (Vector3)(currentMovementInput * Speed * Time.deltaTime);
=======
        Vector3 delta = (Vector3)(currentMovementInput * Speed * Time.deltaTime);
        transform.position += delta;
>>>>>>> Stashed changes
    }
}