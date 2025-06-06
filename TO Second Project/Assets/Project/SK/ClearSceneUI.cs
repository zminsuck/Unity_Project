using UnityEngine;
using TMPro;

public class ClearSceneUI : MonoBehaviour
{
    public TextMeshProUGUI bestKillText;

    void Start()
    {
        // 마우스 커서 잠금 해제 & 보이기
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        int bestKill = PlayerPrefs.GetInt("BestKillCount", 0);
        bestKillText.text = $"Best Kill : {bestKill}";
    }
}
