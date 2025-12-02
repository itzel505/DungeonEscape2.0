using UnityEngine;

public class MobSpawning : MonoBehaviour
{
<<<<<<< Updated upstream
    public Tilemap tilemap;
    public GameObject mobPrefab;
    public Transform player;
    public float spawnDelay = 2f;

    private bool _isSpawning = false;

    public void StartSpawning()
    {
        if (_isSpawning) return;
        InvokeRepeating(nameof(SpawnMob), 0f, spawnDelay);
        _isSpawning = true;
=======
    public GameObject mobPrefab;
    public GameObject player;
    public float spawnInterval = 3f;

    [Header("Spawn Area")]
    [Tooltip("Mobs won't spawn closer than this distance from player")]
    public float minDistanceFromPlayer = 3f;

    [Tooltip("Mobs won't spawn farther than this distance from player")]
    public float maxDistanceFromPlayer = 6f;

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
>>>>>>> Stashed changes
    }

    public void StopSpawning()
    {
        if (!_isSpawning) return;
        CancelInvoke(nameof(SpawnMob));
        _isSpawning = false;
    }

    void SpawnMob()
    {
        if (tilemap == null || mobPrefab == null || player == null) return;

        var bounds = tilemap.cellBounds;
        Vector3Int randomCell = new Vector3Int(
            Random.Range(bounds.xMin, bounds.xMax),
            Random.Range(bounds.yMin, bounds.yMax),
            0
        );

        if (!tilemap.HasTile(randomCell))
            return;

        Vector3 spawnPos = tilemap.GetCellCenterWorld(randomCell);

        GameObject mob = Instantiate(mobPrefab, spawnPos, Quaternion.identity);

        var follow = mob.GetComponent<MobFollowsPlayer>();
        if (follow != null)
            follow.player = player;
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