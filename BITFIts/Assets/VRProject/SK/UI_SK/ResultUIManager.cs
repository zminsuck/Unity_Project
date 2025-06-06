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

        finalScoreText.text = $"최종 점수: {score}";
        finalComboText.text = $"최대 콤보: {combo}";
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
