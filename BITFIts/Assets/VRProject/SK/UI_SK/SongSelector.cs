using UnityEngine;

public class SongSelector : MonoBehaviour
{
    public static string selectedSceneName;

    // 버튼이 클릭되면 씬 이름을 저장
    public void SelectSong(string sceneName)
    {
        selectedSceneName = sceneName;
        Debug.Log("선택된 씬: " + sceneName);
    }
}
