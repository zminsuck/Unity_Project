using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ResultUIManager : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI finalComboText;

    void Start()
    {
        ShowResults();
    }

    void ShowResults()
    {
        int score = ScoreManager.Instance.score;
        int combo = ScoreManager.Instance.combo;

        finalScoreText.text = $"���� ����: {score}";
        finalComboText.text = $"�ִ� �޺�: {combo}";
    }

    public void OnClick_Retry()
    {
        SceneManager.LoadScene("Menu");
    }

    public void OnClick_Quit()
    {
        Application.Quit();
    }
}
