using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;       // ���� ���
    public float smoothSpeed = 0.125f;  // �ε巴�� ���󰡴� �ӵ�
    public Vector3 offset;         // ī�޶� ��ġ ����

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
