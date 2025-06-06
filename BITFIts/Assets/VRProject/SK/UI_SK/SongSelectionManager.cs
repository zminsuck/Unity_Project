using UnityEngine;

public class SongSelectionManager : MonoBehaviour
{
    public static SongSelectionManager Instance;

    private string selectedSongKey;
    private string selectedSceneName;

    private void Awake()
    {
        // ΩÃ±€≈Ê ∆–≈œ
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // æ¿ ¿¸»Ø Ω√ ¿Ø¡ˆ
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
        Debug.Log($"º±≈√µ» ≥Î∑°: {songKey}, æ¿: {sceneName}");
    }

    public string GetSelectedSceneName()
    {
        return selectedSceneName;
    }
}
