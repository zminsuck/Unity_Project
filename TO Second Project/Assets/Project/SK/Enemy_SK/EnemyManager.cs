using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    [Header("목표 설정")]
    public int killGoal = 10;
    private int currentKillCount = 0;

    [Header("UI 연결")]
    public TextMeshProUGUI killCountText; // 여기에 드래그로 KillCountText 연결

    public string nextSceneName = "ClearScene";

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        UpdateKillUI(); // 시작 시도 표시
    }

    public void OnEnemyKilled()
    {
        currentKillCount++;
        Debug.Log($"Current treatment count: {currentKillCount}");

        UpdateKillUI();

        if (currentKillCount >= killGoal)
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }

    void UpdateKillUI()
    {
        if (killCountText != null)
            killCountText.text = $"Current treatment count: {currentKillCount} / {killGoal}";
    }
}

