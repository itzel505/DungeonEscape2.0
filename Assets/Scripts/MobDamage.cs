using UnityEngine;

public class MobDamage : MonoBehaviour
{
    public int damage = 10;
    public float knockbackForce = 3f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // DAMAGE THE PLAYER
            PlayerHealth health = collision.collider.GetComponent<PlayerHealth>();
            if (health != null)
                health.TakeDamage(damage);

            // APPLY KNOCKBACK TO MOB (bounce back)
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 direction = (transform.position - collision.transform.position).normalized;
                rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
            }
        }
    }
}
