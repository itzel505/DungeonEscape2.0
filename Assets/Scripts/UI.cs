using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    [Header("References")]
    public PlayerHealth playerHealth;
    public WaveManager waveManager;

    [Header("Health Bar")]
    public Image healthBarFill;
    public TextMeshProUGUI healthText;

    [Header("Wave Info")]
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI waveTimerText;

    [Header("Enemy Counter")]
    public TextMeshProUGUI enemyCountText;

    [Header("Settings")]
    public Color healthHighColor = new Color(0.2f, 0.8f, 0.2f);   // Green
    public Color healthMediumColor = new Color(0.9f, 0.7f, 0.1f); // Yellow
    public Color healthLowColor = new Color(0.8f, 0.2f, 0.2f);    // Red
    public float lowHealthThreshold = 0.3f;
    public float mediumHealthThreshold = 0.6f;

    private void Update()
    {
        UpdateHealthBar();
        UpdateWaveInfo();
        UpdateEnemyCount();
    }

    private void UpdateHealthBar()
    {
        if (playerHealth == null)
            return;

        float healthPercent = playerHealth.GetHealthPercent();

        // Update fill amount
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = healthPercent;
            healthBarFill.color = GetHealthColor(healthPercent);
        }

        // Update text
        if (healthText != null)
        {
            int current = playerHealth.GetCurrentHealth();
            int max = playerHealth.maxHealth;
            healthText.text = $"{current} / {max}";
        }
    }

    private Color GetHealthColor(float percent)
    {
        if (percent <= lowHealthThreshold)
            return healthLowColor;
        if (percent <= mediumHealthThreshold)
            return healthMediumColor;
        return healthHighColor;
    }

    private void UpdateWaveInfo()
    {
        if (waveManager == null)
            return;

        if (waveText != null)
        {
            int waveNum = waveManager.GetCurrentWaveNumber();
            waveText.text = $"Wave {waveNum}";
        }

        // Timer requires additional wave manager functionality (see updated WaveManager)
        if (waveTimerText != null && waveManager.GetTimeRemaining() >= 0)
        {
            float timeLeft = waveManager.GetTimeRemaining();
            int minutes = Mathf.FloorToInt(timeLeft / 60f);
            int seconds = Mathf.FloorToInt(timeLeft % 60f);
            
            string status = waveManager.IsResting() ? "Rest" : "Wave";
            waveTimerText.text = $"{status}: {minutes}:{seconds:00}";
        }
    }

    private void UpdateEnemyCount()
    {
        if (enemyCountText == null)
            return;

        // Count all active mobs in scene
        int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        enemyCountText.text = $"Enemies: {enemyCount}";
    }
}