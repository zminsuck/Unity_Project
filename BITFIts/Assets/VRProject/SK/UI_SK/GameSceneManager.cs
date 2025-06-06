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
        // 음악이 실제로 재생되기 시작할 때까지 대기
        yield return new WaitUntil(() =>
            musicSource != null &&
            musicSource.clip != null &&
            musicSource.isPlaying &&
            musicSource.time > 0.1f
        );

        hasMusicStarted = true;

        // 음악이 끝날 때까지 대기
        yield return new WaitUntil(() =>
            hasMusicStarted &&
            !musicSource.isPlaying &&
            musicSource.time >= musicSource.clip.length - 0.1f // 거의 끝났을 때만
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
