using UnityEngine;

public class LEDPulse : MonoBehaviour
{
    public Renderer rend;
    public float pulseSpeed = 2f;
    private Material mat;

    void Start()
    {
        mat = rend.material;
    }

    void Update()
    {
        float emission = Mathf.PingPong(Time.time * pulseSpeed, 1f);
        Color baseColor = Color.red; // �⺻ ���� ���� ����
        Color finalColor = baseColor * Mathf.LinearToGammaSpace(emission * 3f);
        mat.SetColor("_EmissionColor", finalColor);
    }
}
