using UnityEngine;
using UnityEngine.Tilemaps;

public class MobSpawning : MonoBehaviour
{
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
}