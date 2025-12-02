using UnityEngine;

/// <summary>
/// Physics-based player movement using Rigidbody2D.
/// Uses the new Input System via KeyboardInput and GamepadInput components.
/// Recommended over Move2D when you need physics interactions (knockback, collisions).
/// </summary>
public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;

    [Header("Components")]
    public Animator Animator;
    public SpriteRenderer SpriteRenderer;
    public KeyboardInput KeyboardInput;
    public GamepadInput GamepadInput;

    private Rigidbody2D rb;
    private Vector2 movementInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        InitializeComponents();
    }

    private void Update()
    {
        GetCombinedInput();
        UpdateAnimator();
        UpdateSpriteDirection();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = movementInput.normalized * moveSpeed;
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
        movementInput = gamepadMovement.sqrMagnitude > 0f ? gamepadMovement : keyboardMovement;
    }

    private void UpdateAnimator()
    {
        if (Animator == null)
            return;

        bool isWalking = movementInput.sqrMagnitude > 0f;
    }

    private void UpdateSpriteDirection()
    {
        if (SpriteRenderer == null)
            return;

        if (movementInput.x < 0f)
            SpriteRenderer.flipX = true;
        else if (movementInput.x > 0f)
            SpriteRenderer.flipX = false;
    }
}