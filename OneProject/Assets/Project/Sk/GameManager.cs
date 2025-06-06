using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI ����")]
    public Button restartButton;
    public GameObject gameoverText;
    public GameObject player;
    public Text killCountText;
    public Text highScoreText;

    [Header("���� Ŭ���� ����")]
    public GameObject gameClearPanel; // Ŭ���� UI �г�
    public int killGoal = 10; // ���� óġ ��ǥ

    private int killCount = 0;
    private int highScore = 0;

    void Start()
    {
        restartButton.gameObject.SetActive(false);
        gameoverText.SetActive(false);

        if (gameClearPanel != null)
            gameClearPanel.SetActive(false);

        restartButton.onClick.AddListener(RestartGame);

        // ����� ���� �ҷ�����
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

        Debug.Log("���� Ŭ����!");
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
            highScoreText.text = "�ְ� ���: " + highScore;
        }
    }

    void SaveKillCount()
    {
        PlayerPrefs.SetInt("KillCount", killCount);

        if (killCount > highScore)
        {
            highScore = killCount;
            PlayerPrefs.SetInt("HighScore", highScore);
            UpdateHighScoreText(); // ���ŵ� �ְ� ��� ȭ�鿡 �ݿ�
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
        Debug.Log("���� ���� �õ�");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }
}
