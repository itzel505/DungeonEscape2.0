using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float shootCooldown = 0.25f;

    private float cooldownTimer;
    void Update()
    {
        cooldownTimer -= Time.deltaTime;

        // Shoot with left mouse button
        if (Input.GetMouseButton(0) && cooldownTimer <= 0f)
        {
            Shoot();
            cooldownTimer = shootCooldown;
        }
    }

    void Shoot()
    {
        // Convert mouse position to world position
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;

        // Calculate direction from firePoint
        Vector2 direction = (mouseWorldPos - firePoint.position).normalized;

        GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        proj.GetComponent<Projectile>().SetDirection(direction);

        Debug.Log("SHOOT FUNCTION CALLED — DIRECTION = " + direction);
    }
}