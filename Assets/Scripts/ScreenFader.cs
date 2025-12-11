using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFader : MonoBehaviour
{
    [Header("Fade Settings")]
    public CanvasGroup canvasGroup;  // Panel's CanvasGroup
    public Image panelImage;         // Optional: the panel's Image
    public float fadeDuration = 1f;  // Duration of fade in seconds
    public bool disableAfterFadeOut = true; // Disable GameObject after fade

    private void Awake()
    {
        // Auto-assign CanvasGroup if not set
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)
            Debug.LogError("No CanvasGroup found! Please add one to the panel.");

        // Auto-assign Image if not set
        if (panelImage == null)
            panelImage = GetComponent<Image>();
    }

    // PUBLIC method for Button
    public void FadeOut()
    {
        if (canvasGroup != null)
            StartCoroutine(FadeRoutine(canvasGroup.alpha, 0f));
    }

    // PUBLIC method for Button
    public void FadeIn()
    {
        if (canvasGroup != null)
        {
            if (disableAfterFadeOut && !gameObject.activeSelf)
                gameObject.SetActive(true); // Reactivate panel if disabled

            StartCoroutine(FadeRoutine(canvasGroup.alpha, 1f));
        }
    }

    private IEnumerator FadeRoutine(float startAlpha, float endAlpha)
    {
        float elapsed = 0f;

        // Enable interaction during fade
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        // Optional: ensure the image alpha starts correctly
        if (panelImage != null)
        {
            Color imgColor = panelImage.color;
            imgColor.a = startAlpha;
            panelImage.color = imgColor;
        }

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / fadeDuration);
            canvasGroup.alpha = alpha;

            // Update Image alpha if assigned
            if (panelImage != null)
            {
                Color imgColor = panelImage.color;
                imgColor.a = alpha;
                panelImage.color = imgColor;
            }

            yield return null;
        }

        canvasGroup.alpha = endAlpha;

        if (panelImage != null)
        {
            Color imgColor = panelImage.color;
            imgColor.a = endAlpha;
            panelImage.color = imgColor;
        }

        // Disable interaction when fully invisible
        if (endAlpha == 0f)
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;

            if (disableAfterFadeOut)
                gameObject.SetActive(false); // Optionally disable panel
        }
    }
}
