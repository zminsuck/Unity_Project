using UnityEngine;
using UnityEngine.Video;

public class VideoAudioSync : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public AudioSource audioSource;
    public MusicSyncSpawner spawner; // ť�� ������ (��Ʈ ����ȭ)

    void Start()
    {
        // ��� �� �ʱ�ȭ
        videoPlayer.Play();
        audioSource.Play();
        spawner.StartSpawning();
    }
}
