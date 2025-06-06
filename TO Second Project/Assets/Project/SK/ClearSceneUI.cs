using UnityEngine;
using TMPro;

public class ClearSceneUI : MonoBehaviour
{
    public TextMeshProUGUI bestKillText;

    void Start()
    {
        // ���콺 Ŀ�� ��� ���� & ���̱�
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        int bestKill = PlayerPrefs.GetInt("BestKillCount", 0);
        bestKillText.text = $"Best Kill : {bestKill}";
    }
}
