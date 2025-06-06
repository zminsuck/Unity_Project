using UnityEngine;
using System.Linq; // Count() 쓰기 위해 추가

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 2f;
    public float spawnRange = 10f;
    public int maxEnemyCount = 10;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 0f, spawnInterval);
    }

    void SpawnEnemy()
    {
        int currentEnemyCount = GameObject.FindGameObjectsWithTag("Enemy")
            .Count(e => e.activeInHierarchy);

        if (currentEnemyCount >= maxEnemyCount)
            return;

        Vector3 spawnPos = new Vector3(
            Random.Range(-spawnRange, spawnRange),
            0f,
            Random.Range(-spawnRange, spawnRange)
        );

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}
