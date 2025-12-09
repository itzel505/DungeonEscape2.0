using UnityEngine;
using UnityEngine.Rendering.Universal;

public class HideInDarkness : MonoBehaviour
{
    [Header("Settings")]
    public float lightRadius = 5f; // Match this to your Point Light 2D outer radius
    public float visibilityBuffer = 0.5f;
    public float fadeSpeed = 5f;

    private SpriteRenderer spriteRenderer;
    private Transform player;
    private Light2D playerLight;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Find player
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;

            // Try to find Light2D on player or its children
            playerLight = playerObj.GetComponentInChildren<Light2D>();
        }
    }

    private void Update()
    {
        if (player == null || spriteRenderer == null)
            return;

        // Get light radius from actual Light2D if available
        float currentRadius = lightRadius;
        if (playerLight != null)
            currentRadius = playerLight.pointLightOuterRadius;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        float targetAlpha = CalculateVisibility(distanceToPlayer, currentRadius);

        // Smoothly fade
        Color color = spriteRenderer.color;
        color.a = Mathf.Lerp(color.a, targetAlpha, Time.deltaTime * fadeSpeed);
        spriteRenderer.color = color;
    }

    private float CalculateVisibility(float distance, float radius)
    {
        float outerEdge = radius + visibilityBuffer;
        float innerEdge = radius * 0.7f;

        if (distance <= innerEdge)
        {
            // Fully visible inside light
            return 1f;
        }
        else if (distance >= outerEdge)
        {
            // Fully hidden outside light
            return 0f;
        }
        else
        {
            // Fade zone
            float t = (distance - innerEdge) / (outerEdge - innerEdge);
            return 1f - t;
        }
    }
}