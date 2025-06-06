using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClearManager : MonoBehaviour
{
    [Header("UI 및 설정")]
    public GameObject clearUI;
    public float delayBeforeNextScene = 3f;
    public string sceneToLoad = "ClearScene";

    [Header("효과음")]
    public AudioClip clearSound;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.spatialBlend = 0f;
        }
    }

    void Start()
    {
        // 마우스 잠금 해제 (클리어 씬에서 클릭 가능하도록)
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void TriggerGameClear()
    {
        if (clearUI != null)
            clearUI.SetActive(true);

        if (clearSound != null && audioSource != null)
            audioSource.PlayOneShot(clearSound);

        Invoke(nameof(LoadNextScene), delayBeforeNextScene);
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    // 나가기 버튼에서 사용할 함수
    public void QuitGame()
    {
        Debug.Log("게임 종료 시도");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
