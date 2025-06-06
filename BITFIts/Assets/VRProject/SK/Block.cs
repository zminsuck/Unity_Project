using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Block : MonoBehaviour
{
    public enum BlockColorType { Red, Blue }
    public BlockColorType blockColor;

    [Header("효과")]
    public AudioClip hitSound;
    public GameObject explosionEffect;

    [Header("블럭 속성")]
    public float moveSpeed = 5f;
    public int scoreValue = 100;

    private AudioSource audioSource;
    private bool isHit = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!isHit)
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isHit) return;

        // 놓친 경우 먼저 검사하고 종료
        if (other.CompareTag("MissZone"))
        {
            ScoreManager.Instance?.ResetCombo();
            Destroy(gameObject);
            return; // 이후 코드 실행 방지
        }

        bool validHandHit =
            (blockColor == BlockColorType.Red && other.CompareTag("RightHand")) ||
            (blockColor == BlockColorType.Blue && other.CompareTag("LeftHand"));

        bool validProjectileHit = false;

        if (other.CompareTag("Projectile"))
        {
            if (other.TryGetComponent<Projectile>(out var proj))
            {
                validProjectileHit =
                    (proj.projectileColor == Projectile.ProjectileColorType.Red && blockColor == BlockColorType.Red) ||
                    (proj.projectileColor == Projectile.ProjectileColorType.Blue && blockColor == BlockColorType.Blue);
            }
        }

        if (validHandHit || validProjectileHit)
        {
            isHit = true;

            ScoreManager.Instance?.AddScore(scoreValue);

            if (explosionEffect != null)
            {
                GameObject fx = Instantiate(explosionEffect, transform.position, Quaternion.identity);
                Destroy(fx, 2f);
            }

            if (hitSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(hitSound);
            }

            if (validProjectileHit)
            {
                Destroy(other.gameObject);
            }

            Destroy(gameObject);
        }
    }

}
