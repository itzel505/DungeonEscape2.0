using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public MobSpawning mobSpawner;

    [Header("Wave Settings")]
    public WaveConfig[] waves;

    [System.Serializable]
    public class WaveConfig
    {
        public string waveName = "Wave";
        public float duration = 60f;
        public float restDurationAfter = 60f;
    }

    // UI-accessible state
    private int currentWaveIndex;
    private float timeRemaining;
    private bool isResting;
    private bool wavesCompleted;

    private void Start()
    {
        if (waves == null || waves.Length == 0)
            SetDefaultWaves();

        StartCoroutine(RunWaves());
    }

    private void SetDefaultWaves()
    {
        waves = new WaveConfig[]
        {
            new WaveConfig { waveName = "Wave 1", duration = 60f, restDurationAfter = 60f },
            new WaveConfig { waveName = "Wave 2", duration = 60f, restDurationAfter = 60f },
            new WaveConfig { waveName = "Wave 3", duration = 180f, restDurationAfter = 0f }
        };
    }

    private IEnumerator RunWaves()
    {
        for (int i = 0; i < waves.Length; i++)
        {
            currentWaveIndex = i;
            WaveConfig wave = waves[i];

            // Start wave
            Debug.Log($"Starting {wave.waveName}");
            isResting = false;
            mobSpawner.StartSpawning();

            // Count down wave duration
            timeRemaining = wave.duration;
            while (timeRemaining > 0f)
            {
                timeRemaining -= Time.deltaTime;
                yield return null;
            }

            mobSpawner.StopSpawning();
            Debug.Log($"{wave.waveName} ended");

            // Rest period
            if (wave.restDurationAfter > 0f)
            {
                Debug.Log($"Rest period: {wave.restDurationAfter} seconds");
                isResting = true;
                timeRemaining = wave.restDurationAfter;
                
                while (timeRemaining > 0f)
                {
                    timeRemaining -= Time.deltaTime;
                    yield return null;
                }
            }
        }

        wavesCompleted = true;
        Debug.Log("All waves completed!");
    }

    // ========== PUBLIC GETTERS FOR UI ==========

    public int GetCurrentWaveNumber()
    {
        return currentWaveIndex + 1;
    }

    public int GetTotalWaves()
    {
        return waves != null ? waves.Length : 0;
    }

    public float GetTimeRemaining()
    {
        return Mathf.Max(0f, timeRemaining);
    }

    public bool IsResting()
    {
        return isResting;
    }

    public bool AreWavesCompleted()
    {
        return wavesCompleted;
    }

    public string GetCurrentWaveName()
    {
        if (waves == null || currentWaveIndex >= waves.Length)
            return "Unknown";
        return waves[currentWaveIndex].waveName;
    }
}