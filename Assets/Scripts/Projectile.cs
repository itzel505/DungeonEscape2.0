using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 20;
    public float lifetime = 2f;

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
        // Move in world space
        transform.Translate(direction * (speed * Time.deltaTime), Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            var health = collision.GetComponent<EnemyHealth>();
            if (health != null)
                health.TakeDamage(damage);

            Destroy(gameObject);
        }
    }
}