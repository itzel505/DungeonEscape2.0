using UnityEngine;
using UnityEngine.Tilemaps;

public class MobSpawning : MonoBehaviour
{
        public Tilemap tilemap;
        public GameObject mobPrefab;
        public Transform player;
        public float spawnDelay = 2f;

        void Start()
        {
            InvokeRepeating(nameof(SpawnMob), spawnDelay, spawnDelay);
        }

        void SpawnMob()
        {
            // Pick a random cell in the tilemap
            var bounds = tilemap.cellBounds;
            Vector3Int randomCell = new Vector3Int(
                Random.Range(bounds.xMin, bounds.xMax),
                Random.Range(bounds.yMin, bounds.yMax),
                0
            );

            // Only spawn if tile exists
            if (!tilemap.HasTile(randomCell))
                return;

            // Convert cell to world position
            Vector3 spawnPos = tilemap.GetCellCenterWorld(randomCell);
            
            GameObject mob = Instantiate(mobPrefab, spawnPos, Quaternion.identity);

            // Give the mob a target to follow
            mob.GetComponent<MobFollowsPlayer>().player = player;
        }
}
