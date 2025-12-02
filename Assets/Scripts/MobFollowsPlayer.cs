using UnityEngine;

public class MobFollowsPlayer : MonoBehaviour
{
<<<<<<< Updated upstream
    public Transform player;
    public float speed = 2f;
=======
    [Header("Movement")]
    public GameObject player;
    public float moveSpeed = 2f;

    [Header("Combat")]
    public int damage = 10;
    public float knockbackForce = 3f;
    public float knockbackDuration = 0.2f;

    private Rigidbody2D rb;
    private bool isKnockedBack;
    private float knockbackTimer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (player == null)
            player = GameObject.FindWithTag("Player");
    }
>>>>>>> Stashed changes

    private void Update()
    {
<<<<<<< Updated upstream
        if (player == null) return;
        
        

        // Move toward the player
        transform.position = Vector3.MoveTowards(
            transform.position,
            player.position,
            speed * Time.deltaTime
        );
=======
        if (player == null)
            return;

        if (isKnockedBack)
        {
            knockbackTimer -= Time.deltaTime;
            if (knockbackTimer <= 0f)
                isKnockedBack = false;
            return;
        }

        FollowPlayer();
    }

    private void FollowPlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
>>>>>>> Stashed changes
    }
}

<<<<<<< Updated upstream
=======
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Player"))
            return;

        DamagePlayer(collision.collider);
        ApplyKnockback(collision.transform.position);
    }

    private void DamagePlayer(Collider2D playerCollider)
    {
        PlayerHealth health = playerCollider.GetComponent<PlayerHealth>();
        if (health != null)
            health.TakeDamage(damage);
    }

    private void ApplyKnockback(Vector3 playerPosition)
    {
        if (rb == null)
            return;

        Vector2 knockDirection = (transform.position - playerPosition).normalized;
        rb.AddForce(knockDirection * knockbackForce, ForceMode2D.Impulse);

        isKnockedBack = true;
        knockbackTimer = knockbackDuration;
    }
}
>>>>>>> Stashed changes
