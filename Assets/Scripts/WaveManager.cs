using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public MobSpawning mobSpawner;

    [Header("Wave Durations (seconds)")]
    public float wave1Duration = 60f;
    public float restAfterWave1 = 60f;

    public float wave2Duration = 60f;
    public float restAfterWave2 = 60f;

    public float wave3Duration = 180f; // 3 minutes

    private void Start()
    {
        StartCoroutine(RunWaves());
    }

    private IEnumerator RunWaves()
    {
        // WAVE 1
        Debug.Log("Starting Wave 1");
        mobSpawner.StartSpawning();
        yield return new WaitForSeconds(wave1Duration);

        Debug.Log("Wave 1 ended. Resting...");
        mobSpawner.StopSpawning();
        yield return new WaitForSeconds(restAfterWave1);

        // WAVE 2
        Debug.Log("Starting Wave 2");
        mobSpawner.StartSpawning();
        yield return new WaitForSeconds(wave2Duration);

        Debug.Log("Wave 2 ended. Resting...");
        mobSpawner.StopSpawning();
        yield return new WaitForSeconds(restAfterWave2);

        // WAVE 3
        Debug.Log("Starting Wave 3");
        mobSpawner.StartSpawning();
        yield return new WaitForSeconds(wave3Duration);

        mobSpawner.StopSpawning();
        Debug.Log("All waves finished!");
    }
}