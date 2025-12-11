using UnityEngine;

public class PotionPlacer : MonoBehaviour
{
    public GameObject PotionPrefab;

    // Area where potions can spawn
    public float minX = -10f;
    public float maxX = 10f;
    public float minY = -5f;
    public float maxY = 5f;

    // How many seconds between potion spawns
    public float spawnInterval = 3f;

    // Internal timer
    private float timer = 1f;

    void Update()
    {
        // Add time each frame
        timer += Time.deltaTime;

        // Spawn only when enough time has passed
        if (timer >= spawnInterval)
        {
            PlaceNewPotion();
            timer = 0f; // reset timer
        }
    }

    private void PlaceNewPotion()
    {
        // Pick a random position on the map
        Vector3 position = new Vector3(
            Random.Range(minX, maxX),
            Random.Range(minY, maxY),
            0
        );

        // Create the potion prefab
        Instantiate(PotionPrefab, position, Quaternion.identity);
    }
}