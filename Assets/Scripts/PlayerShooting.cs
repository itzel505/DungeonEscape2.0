using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float shootCooldown = 0.25f;

    private float cooldownTimer;
    private Vector2 lastMoveDirection = Vector2.right; // Default facing right

    void Update()
    {
        cooldownTimer -= Time.deltaTime;

        // Get movement direction (WASD)
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        // Update facing direction when moving
        if (x != 0 || y != 0)
            lastMoveDirection = new Vector2(x, y).normalized;

        // Shoot with space bar
        if (Input.GetKey(KeyCode.Space) && cooldownTimer <= 0f)
        {
            Shoot();
            cooldownTimer = shootCooldown;
        }
    }

    void Shoot()
    {
        GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        proj.GetComponent<Projectile>().SetDirection(lastMoveDirection);
    }
}