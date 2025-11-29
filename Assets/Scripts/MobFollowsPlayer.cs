using UnityEngine;

public class MobFollowsPlayer : MonoBehaviour
{
    public GameObject player;
    public float moveSpeed = 2f;
    public int damage = 10;
    public float knockbackForce = 3f;
    public float knockbackDuration = 0.2f;

    private Rigidbody2D rb;
    private bool isKnockedBack = false;
    private float knockbackTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Automatically find the player if not assigned in the Inspector
        if (player == null)
        {
            GameObject p = GameObject.FindWithTag("Player");
            if (p != null)
                player = p;
        }
    }

    void Update()
    {
        if (player == null) return;

        // While knocked back, pause following behavior
        if (isKnockedBack)
        {
            knockbackTimer -= Time.deltaTime;
            if (knockbackTimer <= 0)
                isKnockedBack = false;

            return; // stop following the player during knockback
        }

        // Follow player with simple movement
        Vector3 direction = (player.transform.position - transform.position).normalized;
        transform.position += direction * (moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // Damage the player
            PlayerHealth health = collision.collider.GetComponent<PlayerHealth>();
            if (health != null)
                health.TakeDamage(damage);

            // Knockback this mob
            Knockback(collision.transform.position);
        }
    }

    void Knockback(Vector3 playerPos)
    {
        if (rb == null) return;

        Vector2 knockDirection = (transform.position - playerPos).normalized;
        rb.AddForce(knockDirection * knockbackForce, ForceMode2D.Impulse);

        isKnockedBack = true;
        knockbackTimer = knockbackDuration;
    }
}