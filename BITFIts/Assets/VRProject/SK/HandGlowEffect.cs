using UnityEngine;
using System.Collections;

public class HandGlowEffect : MonoBehaviour
{
    public Material handMaterial;
    public Color hitColor = Color.white;
    public float glowTime = 0.2f;

    private Color originalEmission;
    private Coroutine glowRoutine;

    void Start()
    {
        // ���� ��Ƽ���� �߱� �� ����
        originalEmission = handMaterial.GetColor("_EmissionColor");
    }

    public void PlayGlow()
    {
        if (glowRoutine != null) StopCoroutine(glowRoutine);
        glowRoutine = StartCoroutine(GlowEffect());
    }

    private IEnumerator GlowEffect()
    {
        handMaterial.SetColor("_EmissionColor", hitColor);
        yield return new WaitForSeconds(glowTime);
        handMaterial.SetColor("_EmissionColor", originalEmission);
    }
}
