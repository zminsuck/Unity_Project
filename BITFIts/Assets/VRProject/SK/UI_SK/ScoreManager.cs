using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int score = 0;
    public int combo = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddScore(int baseScore)
    {
        combo++;
        int total = baseScore * combo;
        score += total;

        Debug.Log($"+{total}Á¡ | ÄÞº¸: {combo} | ÃÑÁ¡: {score}");

        UI_ScoreDisplay.Instance.UpdateScore(score, combo);
    }

    public void ResetCombo()
    {
        combo = 0;
        UI_ScoreDisplay.Instance.UpdateScore(score, combo);
    }
}
