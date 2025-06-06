using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public float delayBeforeRestart = 3f;
    public string sceneToLoad = "GameOverScene";

    public void TriggerGameOver()
    {
        if (gameOverUI != null)
            gameOverUI.SetActive(true);

        Invoke(nameof(LoadNextScene), delayBeforeRestart);
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
