using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int numberOfEnemiesToSpawn = 5;
    public float spawnRange = 10f;

    void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < numberOfEnemiesToSpawn;)
        {
            // Generate random spawn position within the specified range
            Vector3 randomSpawnPosition = new Vector3(
                Random.Range(-spawnRange, spawnRange),
                0f, // Assuming the ground is at Y = 0
                Random.Range(-spawnRange, spawnRange)
            );

            // Raycast to ensure the spawn position is on the ground
            RaycastHit hit;
            if (Physics.Raycast(randomSpawnPosition + Vector3.up * 10f, Vector3.down, out hit, 100f, LayerMask.GetMask("Ground")))
            {
                randomSpawnPosition.y = hit.point.y;

                // Instantiate the enemy at the valid spawn position
                Instantiate(enemyPrefab, randomSpawnPosition, Quaternion.identity);

                // Increment the counter only if the spawn was successful
                i++;
            }
        }
    }
}
