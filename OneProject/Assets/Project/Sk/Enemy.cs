using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;

    [Header("Health")]
    public int maxHealth = 100;
    private int currentHealth;

    private Transform playerTransform;


    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogWarning("GameOver");
        }

        currentHealth = maxHealth;
    }

    void Update()
    {
        if (playerTransform != null)
        {
            MoveTowardsPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        Vector3 direction = playerTransform.position - transform.position;
        direction.y = 0;

        transform.position += direction.normalized * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.SetActive(false);  // 플레이어 비활성화
            GameOver();  // 게임 오버 처리

        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // GameManager를 통해 처치 수 증가
        GameManager gm = FindFirstObjectByType<GameManager>();
        gm.AddKillCount();  // 몬스터 처치 수 증가

   
        Destroy(gameObject); // 몬스터 죽으면 파괴
    }

    void GameOver()
    {
        // 게임 오버 처리: 플레이어가 죽으면 게임 오버 처리하는 로직
        Debug.Log("GameOver");
    }
}
