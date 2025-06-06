using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_ScoreDisplay : MonoBehaviour
{
    public static UI_ScoreDisplay Instance;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI comboText;

    [Header("콤보 슬라이더")]
    public Slider comboSlider;
    public int maxCombo = 50; // 원하는 최대 콤보 수치

    private void Awake()
    {
        Instance = this;

        if (comboSlider != null)
        {
            comboSlider.minValue = 0;
            comboSlider.maxValue = maxCombo;
            comboSlider.value = 0;
        }
    }

    public void UpdateScore(int score, int combo)
    {
        if (scoreText != null)
            scoreText.text = $"Score: {score}";

        if (comboText != null)
            comboText.text = combo > 0 ? $"Combo: {combo}" : "";

        if (comboSlider != null)
        {
            comboSlider.value = combo;
        }
    }
}
