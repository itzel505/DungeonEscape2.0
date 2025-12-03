using UnityEngine;

public class MobSpawning : MonoBehaviour
{
    public GameObject mobPrefab;
    public GameObject player;
    public float spawnInterval = 3f;

    [Header("Spawn Area")]
    [Tooltip("Mobs won't spawn closer than this distance from player")]
    public float minDistanceFromPlayer = 5f;
    
    [Tooltip("Mobs won't spawn farther than this distance from player")]
    public float maxDistanceFromPlayer = 10f;

    private float timer;
    private bool isSpawning;

    private void Update()
    {
        if (!isSpawning)
            return;

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnMob();
            timer = 0f;
        }
    }

    public void StartSpawning()
    {
        isSpawning = true;
        timer = 0f;
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }

    private void SpawnMob()
    {
        if (mobPrefab == null || player == null)
            return;

        Vector3 spawnPos = GetRandomSpawnPosition();
        GameObject mob = Instantiate(mobPrefab, spawnPos, Quaternion.identity);

        MobFollowsPlayer followScript = mob.GetComponent<MobFollowsPlayer>();
        if (followScript != null)
            followScript.player = player;
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float angle = Random.Range(0f, Mathf.PI * 2f);
        float distance = Random.Range(minDistanceFromPlayer, maxDistanceFromPlayer);

        float offsetX = Mathf.Cos(angle) * distance;
        float offsetY = Mathf.Sin(angle) * distance;

        Vector3 playerPos = player.transform.position;
        return new Vector3(playerPos.x + offsetX, playerPos.y + offsetY, 0f);
    }
}