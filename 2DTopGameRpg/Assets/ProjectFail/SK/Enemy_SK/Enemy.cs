using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 3f;
    public float chaseRange = 5f;
    public float attackRange = 1f;
    public float attackCooldown = 1.0f;
    public int maxHealth = 100;
    public Transform attackPoint;

    private int currentHealth;
    private float lastAttackTime = 0f;
    private Animator animator;
    private Rigidbody2D rb;

    private int attackStep = 0;
    private float comboResetTime = 1.2f;   // 콤보 초기화 시간
    private float lastComboTime = 0f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    private void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= chaseRange && distance > attackRange)
        {
            ChasePlayer();
        }
        else if (distance <= attackRange)
        {
            AttackPlayer();
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy took damage: " + damage);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void ChasePlayer()
    {
        animator.SetBool("isMoving", true);
        Vector2 direction = (player.position - transform.position).normalized;
        rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);
        FlipSprite(direction.x);
    }

    private void AttackPlayer()
    {
        animator.SetBool("isMoving", false);

        if (Time.time >= lastAttackTime + attackCooldown)
        {
            // 콤보 초기화
            if (Time.time - lastComboTime > comboResetTime)
                attackStep = 1;
            else
                attackStep = (attackStep >= 3) ? 1 : attackStep + 1;

            lastAttackTime = Time.time;
            lastComboTime = Time.time;

            animator.SetInteger("AttackIndex", attackStep);
            animator.SetTrigger("AttackTrigger");
        }
    }

    private void FlipSprite(float xDirection)
    {
        if (xDirection != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Sign(xDirection) * Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }

    private void Die()
    {
        Debug.Log("Enemy Died");
        Destroy(gameObject);
    }

    // 애니메이션 이벤트로 호출
        public void DealDamage()
    {
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, attackRange, LayerMask.GetMask("Player"));
        if (hit != null)
        {
            PlayerController pc = hit.GetComponent<PlayerController>();
            if (pc != null)
                pc.TakeDamage(1);
        }
    }
}
