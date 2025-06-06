using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    public float speed = 8f;

    [Header("총알 관련")]
    public GameObject bulletPrefab;
    public float fireInterval = 1f;
    public int bulletCount = 8;
    private float fireTimer = 0f;

    [Header("UI 관련")]
    public Button restartButton;
    public GameObject gameoverText;
    public Image gameoverimage;
    public Text highScoreText;

    [Header("조이스틱 관련")]
    public Joystick joystick;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();

        restartButton.gameObject.SetActive(false);
        gameoverText.SetActive(false);
        gameoverimage.gameObject.SetActive(false);
        highScoreText.gameObject.SetActive(false);

        restartButton.onClick.AddListener(RestartGame);
    }

    void Update()
    {
        if (gameObject.activeSelf)
        {
            Move();
            AutoShoot();
        }
    }

    void Move()
    {
        float xInput = joystick.Horizontal;
        float zInput = joystick.Vertical;

        float xSpeed = xInput * speed;
        float zSpeed = zInput * speed;

        Vector3 velocity = new Vector3(xSpeed, playerRigidbody.linearVelocity.y, zSpeed);
        playerRigidbody.linearVelocity = velocity;
    }

    void AutoShoot()
    {
        fireTimer += Time.deltaTime;

        if (fireTimer >= fireInterval)
        {
            fireTimer = 0f;
            ShootBulletsInCircle();
        }
    }

    void ShootBulletsInCircle()
    {
        float angleStep = 360f / bulletCount;

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = i * angleStep;
            Quaternion rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 spawnDirection = rotation * Vector3.forward;
            Vector3 spawnPos = transform.position + spawnDirection;

            Instantiate(bulletPrefab, spawnPos, rotation);
        }
    }
    GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearest = null;
        float minDist = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = enemy;
            }
        }

        return nearest;
    }

    public void Die()
    {
        gameObject.SetActive(false);
        GameOver();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Die();
        }
    }

    void GameOver()
    {
        Debug.Log("GameOver");

        gameoverText.SetActive(true);
        restartButton.gameObject.SetActive(true);
        gameoverimage.gameObject.SetActive(true);
        highScoreText.gameObject.SetActive(true);
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
