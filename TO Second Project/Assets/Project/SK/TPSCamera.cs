using UnityEngine;

public class TPSCamera : MonoBehaviour // 마우스 회전, 장애물 회피, 마우스 고정 구현
{
    public Transform target;
    public float sensitivity = 2f;
    public float lerpSpeed = 10f;

    public Vector3 offset = new Vector3(0.5f, 1.8f, -3f); // 고정 오프셋
    public float cameraRadius = 0.3f;
    public LayerMask collisionLayers;

    float yaw, pitch;

    void Start()
    {
        LockCursor();
    }

    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        yaw += Input.GetAxis("Mouse X") * sensitivity;
        pitch -= Input.GetAxis("Mouse Y") * sensitivity;
        pitch = Mathf.Clamp(pitch, -20f, 60f);

        Quaternion rot = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 desiredPos = target.position + rot * offset;

        Vector3 start = target.position + Vector3.up * 1.5f;
        Vector3 dir = desiredPos - start;
        float dist = dir.magnitude;

        if (Physics.SphereCast(start, cameraRadius, dir.normalized, out RaycastHit hit, dist, collisionLayers))
        {
            desiredPos = hit.point + hit.normal * 0.1f;
        }

        transform.position = Vector3.Lerp(transform.position, desiredPos, Time.deltaTime * lerpSpeed);
        transform.LookAt(start);
    }
}
