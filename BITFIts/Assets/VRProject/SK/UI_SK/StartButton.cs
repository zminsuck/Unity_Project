using UnityEngine;
using UnityEngine.SceneManagement; //

public class StartButton : MonoBehaviour
{
    public void StartGame()
    {
        if (string.IsNullOrEmpty(SongSelector.selectedSceneName))
        {
            Debug.LogWarning("노래를 선택하세요!");
            return;
        }

        SceneManager.LoadScene(SongSelector.selectedSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
