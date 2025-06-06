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
        Debug.Log($"���� ü��: {currentHealth}");

        // �ǰ� �ִϸ��̼�
        if (animator != null)
            animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        Debug.Log("���� ���!");
        // �״� �ִϸ��̼� ��� �� Destroy ����
        Destroy(gameObject);
    }
}
