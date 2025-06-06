using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("전투 설정")]
    public float chaseRange = 10f;
    public float attackRange = 2f;
    public float attackCooldown = 1.5f;
    public int attackDamage = 10;

    [Header("체력 설정")]
    public int maxHealth = 100;
    private int currentHealth;

    private Transform player;
    private NavMeshAgent agent;
    private Animator animator;
    private float lastAttackTime;

    public System.Action OnEnemyDeath;

    private enum State { Idle, Chase, Attack }
    private State currentState = State.Idle;

    void Start()
    {
        currentHealth = maxHealth;

        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (player == null || !agent.isOnNavMesh) return;

        float distance = Vector3.Distance(transform.position, player.position);

        switch (currentState)
        {
            case State.Idle:
                agent.isStopped = true;
                animator.SetFloat("Speed", 0f);
                animator.SetBool("IsAttacking", false);

                if (distance < chaseRange)
                    currentState = State.Chase;
                break;

            case State.Chase:
                agent.isStopped = false;
                agent.SetDestination(player.position);

                animator.SetFloat("Speed", agent.velocity.magnitude);
                animator.SetBool("IsAttacking", false);

                if (distance < attackRange)
                {
                    currentState = State.Attack;
                    agent.isStopped = true;
                }
                else if (distance > chaseRange)
                {
                    currentState = State.Idle;
                    agent.isStopped = true;
                }
                break;

            case State.Attack:
                transform.LookAt(player);
                animator.SetFloat("Speed", 0f);
                animator.SetBool("IsAttacking", true);

                if (Time.time - lastAttackTime >= attackCooldown)
                {
                    Attack();
                    lastAttackTime = Time.time;
                }

                if (distance > attackRange)
                {
                    currentState = State.Chase;
                    agent.isStopped = false;
                    animator.SetBool("IsAttacking", false);
                }
                break;
        }
    }

    void Attack()
    {
        if (player == null) return;

        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
            Debug.Log("플레이어를 공격했습니다!");
        }
        else
        {
            Debug.LogWarning("PlayerHealth 컴포넌트를 찾을 수 없습니다.");
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);

        if (animator != null)
        {
            animator.SetTrigger("Hit");
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (animator != null) animator.SetTrigger("Die");

        if (agent != null)
        {
            agent.isStopped = true;
            agent.enabled = false;
        }

        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = false;

        EnemyManager.Instance?.OnEnemyKilled();

        OnEnemyDeath?.Invoke();
        Destroy(gameObject, 3f);
    }
}
