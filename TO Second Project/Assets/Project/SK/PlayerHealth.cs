using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private bool isDead = false;
    private Animator animator;
    public HPBar hpBar;

    public GameOverManager gameOverManager; // ← 수동 연결할 필드

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        if (hpBar != null)
        {
            hpBar.target = transform; // 타겟 연결
            hpBar.SetHP(currentHealth, maxHealth);
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }

        if (hpBar != null)
            hpBar.SetHP(currentHealth, maxHealth);
    }

    void Die()
    {
        isDead = true;

        if (animator != null)
            animator.SetTrigger("Die");

        if (gameOverManager != null)
            gameOverManager.TriggerGameOver(); // 여기서 반드시 호출
    }
}
