using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    private Animator animator;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"몬스터 체력: {currentHealth}");

        // 피격 애니메이션
        if (animator != null)
            animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        Debug.Log("몬스터 사망!");
        // 죽는 애니메이션 재생 후 Destroy 가능
        Destroy(gameObject);
    }
}
