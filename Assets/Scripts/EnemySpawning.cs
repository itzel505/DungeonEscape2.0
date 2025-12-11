using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class EnemySpawning : MonoBehaviour
{
    [FormerlySerializedAs("mobPrefab")] public GameObject enemyPrefab;
    public GameObject player;
    public float spawnInterval = 3f;

    [Header("Spawn Area")]
    public float minDistanceFromPlayer = 2f;
    public float maxDistanceFromPlayer = 5f;

    [Header("Collision Detection")]
    public Tilemap collisionTilemap;
    public int maxSpawnAttempts = 30;

    private float timer;
    private bool isSpawning;

    private void Update()
    {
        if (!isSpawning)
            return;

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy();
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

    private void SpawnEnemy()
    {
        if (enemyPrefab == null || player == null)
            return;

        for (int i = 0; i < maxSpawnAttempts; i++)
        {
            Vector3 spawnPos = GetRandomSpawnPosition();

            // Check BEFORE spawning
            if (IsOnCollisionTilemap(spawnPos))
                continue;

            GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

            EnemyFollowsPlayer followScript = enemy.GetComponent<EnemyFollowsPlayer>();
            if (followScript != null)
                followScript.player = player;

            // Add stuck checker to the mob
            MobSpawnStuckChecker stuckChecker = enemy.AddComponent<MobSpawnStuckChecker>();
            stuckChecker.Initialize(collisionTilemap);

            return;
        }

        Debug.LogWarning("EnemySpawning: Could not find valid spawn position after " + maxSpawnAttempts + " attempts");
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

    private bool IsOnCollisionTilemap(Vector3 position)
    {
        if (collisionTilemap == null)
            return false;

        Vector3Int cellPos = collisionTilemap.WorldToCell(position);
        return collisionTilemap.HasTile(cellPos);
    }
}