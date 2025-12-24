using System.Collections;
using UnityEngine;
public class FishSpawner : MonoBehaviour
 {
    public float fishPosY;
    public FishAI fishPrefab;                // Enemy prefab to spawn
    public Rod rod;
    
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
        while(true){
        GameObject[] fishInPond;
        fishInPond = GameObject.FindGameObjectsWithTag("Fish");
        if (fishInPond.Length <= 1)
        {
            int fishToSpawn = Random.Range(minFishPerBatch, maxFishPerBatch);
            for (int i = 0; i < fishToSpawn; i++)
            {
                Vector3 spawnOffset = Random.insideUnitSphere * spawnBatchRadius;
                Vector3 spawnPosition = (Vector3)gameObject.transform.position + spawnOffset;
                spawnPosition.y = fishPosY;

                FishAI newFish = Instantiate(fishPrefab, spawnPosition, Quaternion.identity);
                newFish.rod = rod;
            }
        }
        float interval = Random.Range(minSpawnInterval, maxSpawnInterval);
        yield return new WaitForSeconds(interval);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fish"))
        {
            Debug.Log("Fish");
        }
    }
}
