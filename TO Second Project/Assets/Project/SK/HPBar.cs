using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class HPBar : MonoBehaviour
{
    [Header("�ڵ� ����")]
    public Slider slider;                  // �ڵ����� ã����
    public Transform target;              // Enemy ��ü�� �Ӹ� �� ��ġ
    public Vector3 offset = new Vector3(0, 2.5f, 0); // �⺻ �Ӹ� ��

    void Awake()
    {
        // Slider �ڵ� ����
        if (slider == null)
        {
            slider = GetComponentInChildren<Slider>();
            if (slider == null)
                Debug.LogWarning("HPBar: Slider�� ã�� �� �����ϴ�.", this);
        }

        // target �ڵ� ���� �õ�
        if (target == null && transform.parent != null)
        {
            Transform headTarget = transform.parent.Find("HPBarTarget");
            if (headTarget != null)
            {
                target = headTarget;
            }
            else
            {
                target = transform.parent; // fallback: Enemy �ڽ�
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
