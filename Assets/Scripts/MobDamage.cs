using UnityEngine;

public class MobDamage : MonoBehaviour
{
    [Header("Combat - Mob Deals")]
    public int damage = 10;
    public float knockbackForce = 5f;
    public float knockbackDuration = 0.2f;

    [Header("Combat - Mob Receives")]
    public int collisionDamageFromPlayer = 10;

    private MobFollowsPlayer movementScript;
    private MobHealth mobHealth;

    private void Start()
    {
        movementScript = GetComponent<MobFollowsPlayer>();
        mobHealth = GetComponent<MobHealth>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // 1. PLAY COLLISION SOUND
            if (GameAudio.Instance != null)
                GameAudio.Instance.PlayCollision();

            // 2. DAMAGE THE PLAYER
            PlayerHealth playerHealth = collision.collider.GetComponent<PlayerHealth>();
            if (playerHealth != null)
                playerHealth.TakeDamage(damage);

            // 3. DAMAGE THE MOB
            if (mobHealth != null)
                mobHealth.TakeDamage(collisionDamageFromPlayer);

            // 4. APPLY KNOCKBACK TO MOB (Recoil)
            if (movementScript != null)
            {
                Vector2 direction = (transform.position - collision.transform.position).normalized;
                movementScript.TriggerKnockback(direction, knockbackForce, knockbackDuration);
            }
        }
    }
}