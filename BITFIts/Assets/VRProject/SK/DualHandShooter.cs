using UnityEngine;
using UnityEngine.XR;

public class DualHandShooter : MonoBehaviour
{
    [Header("공통 설정")]
    public GameObject projectilePrefab;
    public float fireForce = 10f;

    [Header("사운드 설정")]
    public AudioClip shootSound;
    private AudioSource audioSource;

    [Header("왼손 설정")]
    public Transform leftFirePoint;

    [Header("오른손 설정")]
    public Transform rightFirePoint;

    private bool wasLeftTriggerHeld = false;
    private bool wasRightTriggerHeld = false;

    void Start()
    {
        // AudioSource는 현재 오브젝트에 부착되어 있다고 가정
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            Debug.LogWarning("AudioSource가 없음! 사운드가 재생되지 않습니다.");
    }

    void Update()
    {
        InputDevice leftHand = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        if (leftHand.TryGetFeatureValue(CommonUsages.triggerButton, out bool leftPressed))
        {
            if (leftPressed && !wasLeftTriggerHeld)
                Shoot(leftFirePoint, Projectile.ProjectileColorType.Blue); //  왼손 → Blue
            wasLeftTriggerHeld = leftPressed;
        }

        InputDevice rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        if (rightHand.TryGetFeatureValue(CommonUsages.triggerButton, out bool rightPressed))
        {
            if (rightPressed && !wasRightTriggerHeld)
                Shoot(rightFirePoint, Projectile.ProjectileColorType.Red); //  오른손 → Red
            wasRightTriggerHeld = rightPressed;
        }

        if (leftFirePoint != null)
            Debug.DrawRay(leftFirePoint.position, leftFirePoint.forward * 2f, Color.blue);
        if (rightFirePoint != null)
            Debug.DrawRay(rightFirePoint.position, rightFirePoint.forward * 2f, Color.red);
    }

    void Shoot(Transform firePoint, Projectile.ProjectileColorType color)
    {
        if (firePoint == null) return;

        Quaternion fixedRotation = Quaternion.Euler(90f, 0f, 0f);
        GameObject proj = Instantiate(projectilePrefab, firePoint.position, fixedRotation);

        Rigidbody rb = proj.GetComponent<Rigidbody>();
        if (rb != null)
            rb.linearVelocity = firePoint.forward * fireForce; //  linearVelocity → velocity

        //  투사체 색상 지정
        Projectile projScript = proj.GetComponent<Projectile>();
        if (projScript != null)
            projScript.projectileColor = color;

        Destroy(proj, 3f);

        if (audioSource != null && shootSound != null)
            audioSource.PlayOneShot(shootSound);
    }

}
