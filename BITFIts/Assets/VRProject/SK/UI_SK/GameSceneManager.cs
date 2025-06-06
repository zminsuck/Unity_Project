using UnityEngine;
using System.Collections;

public class GameSceneManager : MonoBehaviour
{
    public GameObject resultCanvas;
    public AudioSource musicSource;
    public GameObject blockSpawner;

    private bool hasMusicStarted = false;

    void Start()
    {
        resultCanvas.SetActive(false);
        StartCoroutine(MusicRoutine());
    }

    IEnumerator MusicRoutine()
    {
        // ������ ������ ����Ǳ� ������ ������ ���
        yield return new WaitUntil(() =>
            musicSource != null &&
            musicSource.clip != null &&
            musicSource.isPlaying &&
            musicSource.time > 0.1f
        );

        hasMusicStarted = true;

        // ������ ���� ������ ���
        yield return new WaitUntil(() =>
            hasMusicStarted &&
            !musicSource.isPlaying &&
            musicSource.time >= musicSource.clip.length - 0.1f // ���� ������ ����
        );

        ShowResultScreen();
    }

    void ShowResultScreen()
    {
        resultCanvas.SetActive(true);
        if (blockSpawner != null) blockSpawner.SetActive(false);

        foreach (var block in GameObject.FindGameObjectsWithTag("Block"))
        {
            Destroy(block);
        }
    }
}
