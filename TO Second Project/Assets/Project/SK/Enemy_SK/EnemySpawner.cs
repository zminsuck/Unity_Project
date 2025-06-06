using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("���� ����")]
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public float spawnInterval = 5f;
    public int maxEnemies = 20;

    private int currentEnemyCount = 0;

    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 2f, spawnInterval); // 2�� �ĺ��� �ݺ�
    }

    void SpawnEnemy()
    {
        if (currentEnemyCount >= maxEnemies)
            return;

        int index = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[index];

        GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        currentEnemyCount++;

        // ������ ���� ��� �� ȣ���� �ݹ� ���
        Enemy enemy = newEnemy.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.OnEnemyDeath += () => currentEnemyCount--;
        }
    }
}
