using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClearManager : MonoBehaviour
{
    [Header("UI �� ����")]
    public GameObject clearUI;
    public float delayBeforeNextScene = 3f;
    public string sceneToLoad = "ClearScene";

    [Header("ȿ����")]
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
        // ���콺 ��� ���� (Ŭ���� ������ Ŭ�� �����ϵ���)
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

    // ������ ��ư���� ����� �Լ�
    public void QuitGame()
    {
        Debug.Log("���� ���� �õ�");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
