using UnityEngine;

public class MobDamage : MonoBehaviour
{
    [Header("Combat")]
    public int damage = 10;
    public float knockbackForce = 5f; // How hard the MOB bounces back
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
            // 1. DAMAGE THE PLAYER (No Knockback on Player)
            PlayerHealth health = collision.collider.GetComponent<PlayerHealth>();
            if (health != null)
                health.TakeDamage(damage);

            // 2. APPLY KNOCKBACK TO MOB (Recoil)
            if (movementScript != null)
            {
                // Calculate direction: From Player -> To Me (Push me away)
                Vector2 direction = (transform.position - collision.transform.position).normalized;
                
                // Tell the movement script to pause and push back
                movementScript.TriggerKnockback(direction, knockbackForce, knockbackDuration);
            }
        }
    }
}