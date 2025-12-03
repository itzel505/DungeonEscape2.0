using UnityEngine;

public class MobDamage : MonoBehaviour
{
    [Header("Combat")]
    public int damage = 10;
    public float knockbackForce = 5f; // How hard to bounce back
    public float knockbackDuration = 0.2f;

    private MobFollowsPlayer movementScript;

    private void Start()
    {
        // Get reference to the movement script on the SAME object
        movementScript = GetComponent<MobFollowsPlayer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // 1. DAMAGE THE PLAYER
            PlayerHealth health = collision.collider.GetComponent<PlayerHealth>();
            if (health != null)
                health.TakeDamage(damage);

            // 2. APPLY KNOCKBACK TO SELF
            if (movementScript != null)
            {
                // Calculate direction: From Player -> To Me (Push away)
                Vector2 direction = (transform.position - collision.transform.position).normalized;
                
                // Tell the movement script to pause and push back
                movementScript.TriggerKnockback(direction, knockbackForce, knockbackDuration);
            }
        }
    }
}