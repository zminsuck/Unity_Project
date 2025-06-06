using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI 관련")]
    public Button restartButton;
    public GameObject gameoverText;
    public GameObject player;
    public Text killCountText;
    public Text highScoreText;

    [Header("게임 클리어 관련")]
    public GameObject gameClearPanel; // 클리어 UI 패널
    public int killGoal = 10; // 몬스터 처치 목표

    private int killCount = 0;
    private int highScore = 0;

    void Start()
    {
        restartButton.gameObject.SetActive(false);
        gameoverText.SetActive(false);

        if (gameClearPanel != null)
            gameClearPanel.SetActive(false);

        restartButton.onClick.AddListener(RestartGame);

        // 저장된 점수 불러오기
        killCount = PlayerPrefs.GetInt("KillCount", 0);
        highScore = PlayerPrefs.GetInt("HighScore", 0);

        UpdateKillCountText();
        UpdateHighScoreText();
    }

    public void GameOver()
    {
        gameoverText.SetActive(true);
        restartButton.gameObject.SetActive(true);

        if (player != null)
        {
            player.SetActive(false);
        }

        SaveKillCount();
    }

    public void RestartGame()
    {
        killCount = 0;
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("KillCount", 0);
        PlayerPrefs.Save();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AddKillCount()
    {
        killCount++;
        UpdateKillCountText();
        SaveKillCount();

        if (killCount >= killGoal)
        {
            GameClear();
        }
    }

    void GameClear()
    {
        Time.timeScale = 0f;

        if (gameClearPanel != null)
            gameClearPanel.SetActive(true);

        Debug.Log("게임 클리어!");
    }

    void UpdateKillCountText()
    {
        if (killCountText != null)
        {
            killCountText.text = "Kill Monster: " + killCount;
        }
    }

    void UpdateHighScoreText()
    {
        if (highScoreText != null)
        {
            highScoreText.text = "최고 기록: " + highScore;
        }
    }

    void SaveKillCount()
    {
        PlayerPrefs.SetInt("KillCount", killCount);

        if (killCount > highScore)
        {
            highScore = killCount;
            PlayerPrefs.SetInt("HighScore", highScore);
            UpdateHighScoreText(); // 갱신된 최고 기록 화면에 반영
        }

        PlayerPrefs.Save();
    }

    public void ResetKillCount()
    {
        killCount = 0;
        PlayerPrefs.SetInt("KillCount", 0);
        PlayerPrefs.Save();
        UpdateKillCountText();
    }

    public void QuitGame()
    {
        Debug.Log("게임 종료 시도");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }
}
