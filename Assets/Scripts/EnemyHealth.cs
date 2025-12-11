using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 30;
    private int currentHealth;

    [Header("Health Bar")]
    public Vector3 healthBarOffset = new Vector3(0f, 0.8f, 0f);
    public Vector2 healthBarSize = new Vector2(0.5f, 0.08f);
    public bool hideWhenFull = true;
    public Color backgroundColor = new Color(0.2f, 0.2f, 0.2f, 1f);
    public Color fillColor = new Color(0.8f, 0.2f, 0.2f, 1f);
    public int sortingOrder = 100;

    private GameObject healthBarParent;
    private Transform fillTransform;
    private float initialFillWidth;
    private SpriteRenderer mobSpriteRenderer;
    private SpriteRenderer[] healthBarRenderers;

    private void Start()
    {
        currentHealth = maxHealth;
        mobSpriteRenderer = GetComponent<SpriteRenderer>();
        CreateHealthBar();
    }

    private void Update()
    {
        UpdateHealthBarVisibility();
    }

    private void CreateHealthBar()
    {
        // Create parent object
        healthBarParent = new GameObject("HealthBar");
        healthBarParent.transform.SetParent(transform);
        healthBarParent.transform.localPosition = healthBarOffset;

        // Create background
        GameObject background = new GameObject("Background");
        background.transform.SetParent(healthBarParent.transform);
        background.transform.localPosition = Vector3.zero;
        background.transform.localScale = new Vector3(healthBarSize.x, healthBarSize.y, 1f);

        SpriteRenderer bgRenderer = background.AddComponent<SpriteRenderer>();
        bgRenderer.sprite = CreateSquareSprite();
        bgRenderer.color = backgroundColor;
        bgRenderer.sortingOrder = sortingOrder;

        // Create fill (anchored to left side)
        GameObject fill = new GameObject("Fill");
        fill.transform.SetParent(healthBarParent.transform);
        fill.transform.localScale = new Vector3(healthBarSize.x * 0.95f, healthBarSize.y * 0.7f, 1f);

        SpriteRenderer fillRenderer = fill.AddComponent<SpriteRenderer>();
        fillRenderer.sprite = CreateSquareSprite();
        fillRenderer.color = fillColor;
        fillRenderer.sortingOrder = sortingOrder + 1;

        // Position fill at left edge
        float leftEdge = -healthBarSize.x / 2f + (healthBarSize.x * 0.95f) / 2f;
        fill.transform.localPosition = new Vector3(leftEdge, 0f, 0f);

        fillTransform = fill.transform;
        initialFillWidth = healthBarSize.x * 0.95f;

        // Cache health bar renderers
        healthBarRenderers = healthBarParent.GetComponentsInChildren<SpriteRenderer>();

        // Hide if full
        if (hideWhenFull)
            healthBarParent.SetActive(false);
    }

    private Sprite CreateSquareSprite()
    {
        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, Color.white);
        texture.Apply();
        return Sprite.Create(texture, new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f), 1);
    }

    private void UpdateHealthBarVisibility()
    {
        if (healthBarParent == null || mobSpriteRenderer == null || healthBarRenderers == null)
            return;

        // Match health bar alpha to mob alpha
        float mobAlpha = mobSpriteRenderer.color.a;

        foreach (SpriteRenderer sr in healthBarRenderers)
        {
            Color c = sr.color;
            c.a = mobAlpha;
            sr.color = c;
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        // Show health bar when damaged
        if (hideWhenFull && healthBarParent != null)
            healthBarParent.SetActive(true);

        UpdateHealthBarFill();

        if (currentHealth <= 0)
            Die();
    }

    private void UpdateHealthBarFill()
    {
        if (fillTransform == null)
            return;

        float healthPercent = (float)currentHealth / maxHealth;
        
        // Scale the fill bar
        float newWidth = initialFillWidth * healthPercent;
        fillTransform.localScale = new Vector3(newWidth, fillTransform.localScale.y, 1f);

        // Reposition to keep it anchored to the left
        float leftEdge = -healthBarSize.x / 2f + newWidth / 2f;
        fillTransform.localPosition = new Vector3(leftEdge, 0f, 0f);
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} died!");
        Destroy(gameObject);
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GetHealthPercent()
    {
        return (float)currentHealth / maxHealth;
    }
}