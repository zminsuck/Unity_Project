using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("이동 설정")]
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;

    private Vector2 movement;

    [Header("콤보 공격 설정")]
    private int attackStep = 0;
    private float comboResetTime = 1f;
    private float lastAttackTime;

    [Header("참격 이동 설정")]
    public float slashMoveDistance = 0.7f;
    public float slashMoveSpeed = 10f;
    private bool isSlashing = false;
    private Vector2 slashTargetPos;

    [Header("참격 이펙트 설정")]
    public GameObject slashEffectPrefab; // 이펙트 프리팹

    [Header("공격 설정")]
    public float attackRange = 1f;
    public LayerMask enemyLayer;
    public int attackDamage = 1;
    public Transform attackPoint;

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time - lastAttackTime > comboResetTime)
                attackStep = 1;
            else
                attackStep = attackStep >= 3 ? 1 : attackStep + 1;

            lastAttackTime = Time.time;

            animator.SetInteger("AttackIndex", attackStep);
            animator.SetTrigger("AttackTrigger");
        }
    }

    void FixedUpdate()
    {
        if (isSlashing)
        {
            Vector2 newPos = Vector2.MoveTowards(rb.position, slashTargetPos, slashMoveSpeed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);

            if (Vector2.Distance(rb.position, slashTargetPos) < 0.05f)
            {
                isSlashing = false;
            }
        }
        else
        {
            rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
        }
    }

    public void DoSlashMove()
    {
        if (isSlashing) return;

        Vector2 slashDir = movement != Vector2.zero ? movement.normalized : Vector2.right;
        slashTargetPos = rb.position + slashDir * slashMoveDistance;
        isSlashing = true;

        // 이펙트 생성 위치: 공격 기준과 일치
        if (slashEffectPrefab != null && attackPoint != null)
        {
            float angle = Mathf.Atan2(slashDir.y, slashDir.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

            GameObject fx = Instantiate(slashEffectPrefab, attackPoint.position, rotation);
            fx.transform.localScale = new Vector3(attackRange * 2f, 1f, 1f); // 크기 동기화 (선택)
        }
    }

    public void CheckHit()
    {
        // 공격 범위 안에 있는 몬스터 탐지
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>()?.TakeDamage(attackDamage);
        }
    }
}
