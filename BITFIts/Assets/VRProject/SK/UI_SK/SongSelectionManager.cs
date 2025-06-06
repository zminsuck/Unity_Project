using UnityEngine;

public class SongSelectionManager : MonoBehaviour
{
    public static SongSelectionManager Instance;

    private string selectedSongKey;
    private string selectedSceneName;

    private void Awake()
    {
        // �̱��� ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �� ����
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SelectSong(string songKey, string sceneName)
    {
        selectedSongKey = songKey;
        selectedSceneName = sceneName;
        Debug.Log($"���õ� �뷡: {songKey}, ��: {sceneName}");
    }

    public string GetSelectedSceneName()
    {
        return selectedSceneName;
    }
}
