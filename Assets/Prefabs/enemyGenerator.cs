using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [System.Serializable]
    public class EnemySpawnInfo
    {
        public GameObject enemyPrefab;
        public int count;
    }

    public EnemySpawnInfo[] enemiesToSpawn;
    public Vector3 spawnAreaMin;
    public Vector3 spawnAreaMax;
    public Vector3 enemyScale = new Vector3(0.5f, 0.5f, 0.5f); // Set the desired scale here

    void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        foreach (EnemySpawnInfo spawnInfo in enemiesToSpawn)
        {
            for (int i = 0; i < spawnInfo.count; i++)
            {
                SpawnEnemy(spawnInfo.enemyPrefab);
            }
        }
    }

    void SpawnEnemy(GameObject enemy)
    {
        Vector3 spawnPosition = new Vector3(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y),
            Random.Range(spawnAreaMin.z, spawnAreaMax.z)
        );

        GameObject spawnedEnemy = Instantiate(enemy, spawnPosition, Quaternion.identity);
        spawnedEnemy.transform.localScale = enemyScale; // Apply the scale
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((spawnAreaMin + spawnAreaMax) / 2, spawnAreaMax - spawnAreaMin);
    }
}
