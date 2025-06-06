using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;
    private int comboStep = 0;
    private bool isAttacking = false;
    private float comboTimer = 0f;
    public float comboResetTime = 1f;

    [Header("공격 판정")]
    public Transform attackPoint;
    public float attackRange = 1.2f;
    public int damage = 10;
    public LayerMask enemyLayer;

    [Header("효과음")]
    public AudioClip attackSound;
    private AudioSource audioSource;

    [Header("타격 이펙트들")]
    public GameObject[] hitEffectPrefabs; // 이펙트 프리팹

    void Start()
    {
        anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.LogWarning("Animator를 찾을 수 없습니다.");
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.spatialBlend = 0f; // 2D 사운드
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

    // 수정된 부분: 타격 이펙트 생성 포함
    public void DealDamage()
    {
        Collider[] enemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider enemy in enemies)
        {
            Enemy enemyComponent = enemy.GetComponent<Enemy>();
            if (enemyComponent != null)
            {
                enemyComponent.TakeDamage(damage);

                // 콤보 번호에 따라 다른 이펙트 선택
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
