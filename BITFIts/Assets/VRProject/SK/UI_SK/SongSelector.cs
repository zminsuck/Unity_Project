using UnityEngine;

public class SongSelector : MonoBehaviour
{
    public static string selectedSceneName;

    // ��ư�� Ŭ���Ǹ� �� �̸��� ����
    public void SelectSong(string sceneName)
    {
        selectedSceneName = sceneName;
        Debug.Log("���õ� ��: " + sceneName);
    }
}
