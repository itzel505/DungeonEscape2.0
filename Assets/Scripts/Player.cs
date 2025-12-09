using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;

    [Header("Pickup")]
    public Transform holdPoint;
    public float pickupRange = 3f;
    public KeyCode interactKey = KeyCode.E;

    [Header("Components")]
    public Animator Animator;
    public SpriteRenderer SpriteRenderer;
    public KeyboardInput KeyboardInput;
    public GamepadInput GamepadInput;

    private Rigidbody2D rb;
    private Vector2 movementInput;
    private GameObject currentItem;
    private Vector2 lastFacingDirection = Vector2.down;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        InitializeComponents();
    }

    private void Update()
    {
        GetCombinedInput();
        UpdateFacingDirection();
        UpdateAnimator();
        UpdateSpriteDirection();
        HandlePickupInput();
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

        movementInput = gamepadMovement.sqrMagnitude > 0f ? gamepadMovement : keyboardMovement;
    }

    private void UpdateFacingDirection()
    {
        if (movementInput.sqrMagnitude > 0f)
            lastFacingDirection = movementInput.normalized;
    }

    private void UpdateAnimator()
    {
        if (Animator == null)
            return;

        bool isWalking = movementInput.sqrMagnitude > 0f;
        Animator.SetBool("IsWalking", isWalking);
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

    private void HandlePickupInput()
    {
        if (Input.GetKeyDown(interactKey))
        {
            if (currentItem == null)
                TryPickup();
            else
                DropItem();
        }
    }

    private void TryPickup()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, lastFacingDirection, pickupRange);

        if (hit.collider != null && hit.collider.CompareTag("Pickup"))
        {
            currentItem = hit.collider.gameObject;
            Rigidbody2D itemRb = currentItem.GetComponent<Rigidbody2D>();
            
            if (itemRb != null)
                itemRb.simulated = false;

            currentItem.transform.SetParent(holdPoint);
            currentItem.transform.localPosition = Vector3.zero;
            currentItem.transform.localRotation = Quaternion.identity;
        }
    }

    private void DropItem()
    {
        Rigidbody2D itemRb = currentItem.GetComponent<Rigidbody2D>();
        
        if (itemRb != null)
        {
            itemRb.simulated = true;
            itemRb.AddForce(lastFacingDirection * 2f, ForceMode2D.Impulse);
        }

        currentItem.transform.SetParent(null);
        currentItem = null;
    }

    public bool IsHoldingItem()
    {
        return currentItem != null;
    }
}