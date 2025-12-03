using UnityEngine;
using UnityEngine.Tilemaps;

public class MobSpawnStuckChecker : MonoBehaviour
{
    public float checkDelay = 0.5f;
    public float stuckThreshold = 0.001f;

    private Tilemap collisionTilemap;
    private Vector3 lastPosition;
    private float timer;
    private bool initialized;

    public void Initialize(Tilemap tilemap)
    {
        collisionTilemap = tilemap;
        lastPosition = transform.position;
        initialized = true;
    }

    private void Update()
    {
        if (!initialized)
            return;

        timer += Time.deltaTime;

        if (timer >= checkDelay)
        {
            CheckIfStuck();
            timer = 0f;
        }
    }

    private void CheckIfStuck()
    {
        // Check if inside collision tilemap
        if (collisionTilemap != null)
        {
            Vector3Int cellPos = collisionTilemap.WorldToCell(transform.position);
            if (collisionTilemap.HasTile(cellPos))
            {
                Debug.Log("Mob destroyed: inside wall");
                Destroy(gameObject);
                return;
            }
        }

        // Check if not moving (stuck between tiles)
        float distanceMoved = Vector3.Distance(transform.position, lastPosition);
        if (distanceMoved < stuckThreshold)
        {
            // Double check - are we actually trying to move?
            MobFollowsPlayer followScript = GetComponent<MobFollowsPlayer>();
            if (followScript != null && followScript.player != null)
            {
                float distanceToPlayer = Vector3.Distance(transform.position, followScript.player.transform.position);
                
                // If we should be moving toward player but aren't, we're stuck
                if (distanceToPlayer > 1f)
                {
                    Debug.Log("Mob destroyed: stuck and not moving");
                    Destroy(gameObject);
                    return;
                }
            }
        }

        lastPosition = transform.position;
    }
}