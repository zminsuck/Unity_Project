using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("스폰 설정")]
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public float spawnInterval = 5f;
    public int maxEnemies = 20;

    private int currentEnemyCount = 0;

    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 2f, spawnInterval); // 2초 후부터 반복
    }

    void SpawnEnemy()
    {
        if (currentEnemyCount >= maxEnemies)
            return;

        int index = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[index];

        GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        currentEnemyCount++;

        // 스폰된 적이 사망 시 호출할 콜백 등록
        Enemy enemy = newEnemy.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.OnEnemyDeath += () => currentEnemyCount--;
        }
    }
}
