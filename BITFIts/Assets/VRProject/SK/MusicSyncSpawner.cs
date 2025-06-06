using UnityEngine;
using System.Collections.Generic;

public class MusicSyncSpawner : MonoBehaviour
{
    public GameObject[] cubes;         // ť�� ������ (BLUE, RED)
    public Transform[] points;         // ���� ��ġ�� (¦��: Blue, Ȧ��: Red)
    public AudioSource musicSource;    // ���� �ҽ� ����
    public GameObject resultUI;

    public float bpm = 120f;           // ���� BPM
    private float beatInterval;        // 1���� ���� (��)

    private float timer;
    private bool started = false;

    public bool autoStartMusic = true; // �׽�Ʈ�� �ڵ� ���

    void Start()
    {
        beatInterval = 60f / bpm;

        if (points.Length < 2)
        {
            enabled = false;
            return;
        }

        if (autoStartMusic && !musicSource.isPlaying)
        {
            musicSource.Play();
        }
    }
    void Update()
    {
        if (!started)
        {
            if (musicSource.isPlaying)
            {
                started = true;
                timer = 0f;
            }
            return;
        }

        timer += Time.deltaTime;

        while (timer >= beatInterval)
        {
            SpawnBlock();
            timer -= beatInterval;
        }

        // ������ ������ ��� ��� UI Ȱ��ȭ
        if (started && !musicSource.isPlaying && musicSource.time > 0f)
        {
            resultUI.SetActive(true);
            enabled = false;
        }
    }

    public void StartSpawning()
    {
        started = true;
        timer = 0f;
    }

    void SpawnBlock()
    {
        if (cubes.Length == 0 || points.Length == 0) return;

        int prefabIndex = Random.Range(0, cubes.Length);
        GameObject prefab = cubes[prefabIndex];
        if (prefab == null) return;

        Block blockData = prefab.GetComponent<Block>();
        if (blockData == null) return;

        List<int> possibleIndexes = new List<int>();

        for (int i = 0; i < points.Length; i++)
        {
            if (blockData.blockColor == Block.BlockColorType.Blue && i % 2 == 0)
                possibleIndexes.Add(i); // ¦�� �ε����� Blue��
            else if (blockData.blockColor == Block.BlockColorType.Red && i % 2 == 1)
                possibleIndexes.Add(i); // Ȧ�� �ε����� Red��
        }

        float angle = 90f * Random.Range(0, 4);
        Quaternion rotation = Quaternion.Euler(0f, 180f, angle);

        while (possibleIndexes.Count > 0)
        {
            int rand = Random.Range(0, possibleIndexes.Count);
            int index = possibleIndexes[rand];
            Transform point = points[index];

            Collider[] hit = Physics.OverlapSphere(point.position, 0.5f);
            bool hasBlock = false;
            foreach (var h in hit)
            {
                if (h.CompareTag("Block"))
                {
                    hasBlock = true;
                    break;
                }
            }

            if (!hasBlock)
            {
                GameObject spawned = Instantiate(prefab, point.position, rotation);
                Destroy(spawned, 5f);
                return;
            }
            else
            {
                possibleIndexes.RemoveAt(rand);
            }
        }
    }
}
