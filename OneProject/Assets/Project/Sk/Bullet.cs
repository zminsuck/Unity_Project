using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 1;
    public float lifeTime = 3f;
    private GameManager gm;

    public GameObject hitEffectPrefab;

    [Header("사운드 관련")]
    public AudioClip hitSound; // 총알 충돌 사운드
    private AudioSource audioSource; // 오디오 소스

    [Header("발사 이펙트")]
    public GameObject spawnEffect;

    void Start()
    {
        if (spawnEffect != null)
        {
            GameObject fx = Instantiate(spawnEffect, transform.position, transform.rotation);
            Destroy(fx, 1f);
        }

        Destroy(gameObject, lifeTime);
        gm = FindFirstObjectByType<GameManager>();

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                if (hitEffectPrefab != null)
                {
                    Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
                }

                if (hitSound != null)
                {
                    audioSource.PlayOneShot(hitSound); // 사운드 재생
                }

                enemy.gameObject.SetActive(false);
                gm.AddKillCount();

                Destroy(gameObject, 1.0f); // 사운드 재생 후 약간의 지연 후 파괴
            }
        }
    }
}
