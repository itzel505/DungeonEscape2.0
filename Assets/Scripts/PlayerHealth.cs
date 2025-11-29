
using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;

    public SpriteRenderer spriteRenderer;
    public float flashDuration = 0.1f;
    private Color defaultColor;

    void Start()
    {
        currentHealth = maxHealth;
        defaultColor = spriteRenderer.color;  // Save real starting color
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Player took damage: " + currentHealth);

        Flash();

        if (currentHealth <= 0)
        {
            Debug.Log("Player died!");
            // Add death logic here
        }
    }

    public void Flash()
    {
        StopAllCoroutines();
        StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = defaultColor;
    }
}
