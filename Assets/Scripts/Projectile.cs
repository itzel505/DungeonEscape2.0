using UnityEngine;

public class Projectile : MonoBehaviour
    
{
    public float speed = 10f;
    public int damage = 20;
    public float lifetime = 2f;  // destroy after 2 seconds

    private Vector2 direction;

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(direction * (speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If it hits a mob
        if (collision.CompareTag("Mob"))
        {
            // If the mob has health, apply damage
            var health = collision.GetComponent<MobHealth>();
            if (health != null)
                health.TakeDamage(damage);

            Destroy(gameObject); // Remove projectile
        }
    }
}
