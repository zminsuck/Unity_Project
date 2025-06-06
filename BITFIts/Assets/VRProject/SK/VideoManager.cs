using UnityEngine;
using UnityEngine.Video;

public class VideoAudioSync : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public AudioSource audioSource;
    public MusicSyncSpawner spawner; // 큐브 생성기 (비트 동기화)

    void Start()
    {
        // 재생 전 초기화
        videoPlayer.Play();
        audioSource.Play();
        spawner.StartSpawning();
    }
}
