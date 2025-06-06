using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private bool isDead = false;
    private Animator animator;
    public HPBar hpBar;

    public GameOverManager gameOverManager; // �� ���� ������ �ʵ�

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        if (hpBar != null)
        {
            hpBar.target = transform; // Ÿ�� ����
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
            gameOverManager.TriggerGameOver(); // ���⼭ �ݵ�� ȣ��
    }
}
