using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public MobSpawning mobSpawner;

    public float wave1Duration = 60f;
    public float restAfterWave1 = 60f;

    public float wave2Duration = 60f;
    public float restAfterWave2 = 60f;

    public float wave3Duration = 180f; // 3 minutes

    private int currentWave = 0;

    private void Start()
    {
        StartCoroutine(RunWaves());
    }

    private IEnumerator RunWaves()
    {
        // WAVE 1
        currentWave = 1;
        mobSpawner.StartSpawning();
        yield return new WaitForSeconds(wave1Duration);

        mobSpawner.StopSpawning();
        yield return new WaitForSeconds(restAfterWave1);

        // WAVE 2
        currentWave = 2;
        mobSpawner.StartSpawning();
        yield return new WaitForSeconds(wave2Duration);

        mobSpawner.StopSpawning();
        yield return new WaitForSeconds(restAfterWave2);

        // WAVE 3
        currentWave = 3;
        mobSpawner.StartSpawning();
        yield return new WaitForSeconds(wave3Duration);

        mobSpawner.StopSpawning();
        Debug.Log("All waves finished!");
    }
}