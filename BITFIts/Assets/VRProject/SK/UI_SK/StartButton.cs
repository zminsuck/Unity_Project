using UnityEngine;
using UnityEngine.SceneManagement; //

public class StartButton : MonoBehaviour
{
    public void StartGame()
    {
        if (string.IsNullOrEmpty(SongSelector.selectedSceneName))
        {
            Debug.LogWarning("�뷡�� �����ϼ���!");
            return;
        }

        SceneManager.LoadScene(SongSelector.selectedSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
