using UnityEngine;
using UnityEngine.Tilemaps;

public class MobSpawning : MonoBehaviour
{
    public GameObject mobPrefab;       // prefab to spawn
    public GameObject player;          // player reference as GameObject
    public float spawnInterval = 3f;   // seconds between spawns
    public Vector2 spawnOffset;        // how far from spawner to spawn

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnMob();
            timer = 0f;
        }
    }

    void SpawnMob()
    {
        Vector3 spawnPos = (Vector2)transform.position + spawnOffset;

        // Instantiate the mob prefab at the spawn position
        GameObject mob = Instantiate(mobPrefab, spawnPos, Quaternion.identity);

        // Give the mob a reference to the player
        MobFollowsPlayer follow = mob.GetComponent<MobFollowsPlayer>();
        if (follow != null)
        {
            follow.player = player; // Assign the entire GameObject (not Transform)
        }
    }
}