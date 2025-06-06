using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class HPBar : MonoBehaviour
{
    [Header("자동 설정")]
    public Slider slider;                  // 자동으로 찾아짐
    public Transform target;              // Enemy 본체나 머리 위 위치
    public Vector3 offset = new Vector3(0, 2.5f, 0); // 기본 머리 위

    void Awake()
    {
        // Slider 자동 연결
        if (slider == null)
        {
            slider = GetComponentInChildren<Slider>();
            if (slider == null)
                Debug.LogWarning("HPBar: Slider를 찾을 수 없습니다.", this);
        }

        // target 자동 연결 시도
        if (target == null && transform.parent != null)
        {
            Transform headTarget = transform.parent.Find("HPBarTarget");
            if (headTarget != null)
            {
                target = headTarget;
            }
            else
            {
                target = transform.parent; // fallback: Enemy 자신
            }
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 worldPos = target.position + offset;
        transform.position = worldPos;
        transform.forward = Camera.main.transform.forward;
    }

    public void SetHP(float current, float max)
    {
        if (slider != null)
        {
            slider.value = current / max;
        }
    }
}
