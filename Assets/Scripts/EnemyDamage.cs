using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [Header("Combat - Enemy Deals")]
    public int damage = 10;
    public float knockbackForce = 5f;
    public float knockbackDuration = 0.2f;

    [Header("Combat - Enemy Receives")]
    public int collisionDamageFromPlayer = 10;

    private EnemyFollowsPlayer movementScript;
    private EnemyHealth enemyHealth;

    private void Start()
    {
        movementScript = GetComponent<EnemyFollowsPlayer>();
        enemyHealth = GetComponent<EnemyHealth>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // 1. DAMAGE THE PLAYER
            PlayerHealth playerHealth = collision.collider.GetComponent<PlayerHealth>();
            if (playerHealth != null)
                playerHealth.TakeDamage(damage);

            // 2. DAMAGE THE MOB
            if (enemyHealth != null)
                enemyHealth.TakeDamage(collisionDamageFromPlayer);

            // 3. APPLY KNOCKBACK TO MOB (Recoil)
            if (movementScript != null)
            {
                Vector2 direction = (transform.position - collision.transform.position).normalized;
                movementScript.TriggerKnockback(direction, knockbackForce, knockbackDuration);
            }
        }
    }
}