using System.Collections;
using UnityEngine;
public class MonsterSpawner : MonoBehaviour
 {
    public float fishPosY;
    public GameObject fishPrefab;                // Enemy prefab to spawn
    
    public int minFishPerBatch = 3;            // Minimum number of enemies per batch
    public int maxFishPerBatch = 6;            // Maximum number of enemies per batch

    
    public float minSpawnInterval = 3f;           // Minimum interval between spawns
    public float maxSpawnInterval = 5f;           // Maximum interval between spawns
    
    public float spawnBatchRadius = 2f;           // Radius within which enemies will be spawned in each batch

    public void Start()
    {
        StartCoroutine(SpawnFish());
    }
    private IEnumerator SpawnFish()
    {
        float interval = Random.Range(minSpawnInterval, maxSpawnInterval);
        yield return new WaitForSeconds(interval);

        int fishToSpawn = Random.Range(minFishPerBatch, maxFishPerBatch);

        for (int i = 0; i < fishToSpawn; i++)
        {
            Vector3 spawnOffset = Random.insideUnitCircle * spawnBatchRadius;
            Vector3 spawnPosition = (Vector3)gameObject.transform.position + spawnOffset;
            spawnPosition.y = fishPosY;

            Instantiate(fishPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
