using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;
    private int comboStep = 0;
    private bool isAttacking = false;
    private float comboTimer = 0f;
    public float comboResetTime = 1f;

    [Header("���� ����")]
    public Transform attackPoint;
    public float attackRange = 1.2f;
    public int damage = 10;
    public LayerMask enemyLayer;

    [Header("ȿ����")]
    public AudioClip attackSound;
    private AudioSource audioSource;

    [Header("Ÿ�� ����Ʈ��")]
    public GameObject[] hitEffectPrefabs; // ����Ʈ ������

    void Start()
    {
        anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.LogWarning("Animator�� ã�� �� �����ϴ�.");
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.spatialBlend = 0f; // 2D ����
        }
    }

    void Update()
    {
        HandleComboInput();
        UpdateComboTimer();
    }

    void HandleComboInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isAttacking)
            {
                comboStep = 0;
                anim.ResetTrigger("attack");
                anim.SetInteger("combo", comboStep);
                anim.SetTrigger("attack");

                isAttacking = true;
                comboTimer = 0f;

                PlayAttackSound();
            }
            else if (comboStep < 2)
            {
                comboStep++;
                anim.SetInteger("combo", comboStep);
                comboTimer = 0f;

                PlayAttackSound();
            }
        }
    }

    void UpdateComboTimer()
    {
        if (isAttacking)
        {
            comboTimer += Time.deltaTime;
            if (comboTimer > comboResetTime)
            {
                EndCombo();
            }
        }
    }

    public void EndCombo()
    {
        isAttacking = false;
        comboStep = 0;
        comboTimer = 0f;

        anim.SetInteger("combo", 0);
        anim.ResetTrigger("attack");
    }

    // ������ �κ�: Ÿ�� ����Ʈ ���� ����
    public void DealDamage()
    {
        Collider[] enemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider enemy in enemies)
        {
            Enemy enemyComponent = enemy.GetComponent<Enemy>();
            if (enemyComponent != null)
            {
                enemyComponent.TakeDamage(damage);

                // �޺� ��ȣ�� ���� �ٸ� ����Ʈ ����
                int index = Mathf.Clamp(comboStep, 0, hitEffectPrefabs.Length - 1);
                GameObject selectedEffect = hitEffectPrefabs[index];

                if (selectedEffect != null)
                {
                    Vector3 hitPos = enemy.ClosestPoint(attackPoint.position);
                    GameObject effect = Instantiate(selectedEffect, hitPos, Quaternion.identity);
                    Destroy(effect, 1.5f);
                }
            }
        }
    }

    void PlayAttackSound()
    {
        if (attackSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(attackSound);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}
