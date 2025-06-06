using UnityEngine;

public class PlayerLookRotation : MonoBehaviour
{
    public Transform cameraTransform; // TPS ī�޶� ����

    void Update()
    {
        Vector3 lookDir = cameraTransform.forward;
        lookDir.y = 0f; // ���� ȸ����

        if (lookDir.sqrMagnitude > 0.01f)
        {
            Quaternion targetRot = Quaternion.LookRotation(lookDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 10f);
        }
    }
}
