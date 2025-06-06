using UnityEngine;

public class PlayerLookRotation : MonoBehaviour
{
    public Transform cameraTransform; // TPS 카메라를 지정

    void Update()
    {
        Vector3 lookDir = cameraTransform.forward;
        lookDir.y = 0f; // 수평 회전만

        if (lookDir.sqrMagnitude > 0.01f)
        {
            Quaternion targetRot = Quaternion.LookRotation(lookDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 10f);
        }
    }
}
