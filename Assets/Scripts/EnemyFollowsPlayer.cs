using UnityEngine;
using System.Collections;

public class EnemyFollowsPlayer : MonoBehaviour
{
    [Header("Movement")]
    public GameObject player;
    public float moveSpeed = 2f;

    private Rigidbody2D rb;
    private bool isKnockedBack = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (player == null)
            player = GameObject.FindWithTag("Player");
    }

    private void FixedUpdate()
    {
        if (player == null) return;

        // If we are currently bouncing back, DO NOT run movement logic.
        if (isKnockedBack) return;

        MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        // Use 'velocity' for Unity 2022/2021. Use 'linearVelocity' for Unity 6.
        rb.linearVelocity = direction * moveSpeed;
    }

    // ---------------------------------------------------------
    // PUBLIC FUNCTION: Called by MobDamage
    // ---------------------------------------------------------
    public void TriggerKnockback(Vector2 direction, float force, float duration)
    {
        StartCoroutine(KnockbackRoutine(direction, force, duration));
    }

    private IEnumerator KnockbackRoutine(Vector2 direction, float force, float duration)
    {
        isKnockedBack = true;

        // 1. Reset velocity to ensure clean knockback
        rb.linearVelocity = Vector2.zero;

        // 2. Apply the force to the MOB
        rb.AddForce(direction * force, ForceMode2D.Impulse);

        // 3. Wait for the duration
        yield return new WaitForSeconds(duration);

        // 4. Regain control
        rb.linearVelocity = Vector2.zero;
        isKnockedBack = false;
    }
}