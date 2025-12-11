using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 100;
    private int currentHealth;

    [Header("Damage Flash")]
    public SpriteRenderer spriteRenderer;
    public float flashDuration = 0.1f;
    private Color defaultColor;
    
    [Header("Potion Pickup")]
    public int healAmount = 20; // NEW: How much a potion heals the player


    private void Start()
    {
        currentHealth = maxHealth;
        
        if (spriteRenderer != null)
            defaultColor = spriteRenderer.color;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log($"Player took {amount} damage. Health: {currentHealth}/{maxHealth}");

        Flash();

        if (currentHealth <= 0)
            Die();
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        Debug.Log($"Player healed {amount}. Health: {currentHealth}/{maxHealth}");
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GetHealthPercent()
    {
        return (float)currentHealth / maxHealth;
    }

    private void Flash()
    {
        if (spriteRenderer == null)
            return;

        StopAllCoroutines();
        StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = defaultColor;
    }

    private void Die()
    {
        Debug.Log("Player died!");
        // TODO: Add death logic (respawn, game over screen, etc.)
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //  Check if we collided with a potion
        if (collision.CompareTag("Potion"))
        {
            Debug.Log("Player picked up a potion!");

            // Heal the player
            Heal(healAmount);

            // Remove the potion from the map
            Destroy(collision.gameObject);
        }
    }
}