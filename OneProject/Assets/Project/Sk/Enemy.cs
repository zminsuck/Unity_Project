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
            other.gameObject.SetActive(false);  // �÷��̾� ��Ȱ��ȭ
            GameOver();  // ���� ���� ó��

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
        // GameManager�� ���� óġ �� ����
        GameManager gm = FindFirstObjectByType<GameManager>();
        gm.AddKillCount();  // ���� óġ �� ����

   
        Destroy(gameObject); // ���� ������ �ı�
    }

    void GameOver()
    {
        // ���� ���� ó��: �÷��̾ ������ ���� ���� ó���ϴ� ����
        Debug.Log("GameOver");
    }
}
