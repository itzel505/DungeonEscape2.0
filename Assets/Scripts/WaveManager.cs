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

    private int currentWaveIndex;

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

            Debug.Log($"Starting {wave.waveName}");
            mobSpawner.StartSpawning();

            yield return new WaitForSeconds(wave.duration);

            mobSpawner.StopSpawning();
            Debug.Log($"{wave.waveName} ended");

            if (wave.restDurationAfter > 0f)
            {
                Debug.Log($"Rest period: {wave.restDurationAfter} seconds");
                yield return new WaitForSeconds(wave.restDurationAfter);
            }
        }

        Debug.Log("All waves completed!");
    }

    public int GetCurrentWaveNumber()
    {
        return currentWaveIndex + 1;
    }
}