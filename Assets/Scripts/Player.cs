using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 6f;
    
    private Rigidbody2D _rb;
    private Vector2 _movementInput;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        _movementInput.x = Input.GetAxisRaw("Horizontal");
        _movementInput.y = Input.GetAxisRaw("Vertical");
    }
    
    private void FixedUpdate()
    {
        _rb.linearVelocity = _movementInput.normalized * moveSpeed;
    }
}