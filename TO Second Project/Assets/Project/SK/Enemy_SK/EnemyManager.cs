using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    [Header("��ǥ ����")]
    public int killGoal = 10;
    private int currentKillCount = 0;

    [Header("UI ����")]
    public TextMeshProUGUI killCountText; // ���⿡ �巡�׷� KillCountText ����

    public string nextSceneName = "ClearScene";

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        UpdateKillUI(); // ���� �õ� ǥ��
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

