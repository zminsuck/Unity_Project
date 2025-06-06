using UnityEngine;

public class Player : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float runSpeed = 6f;

    private Rigidbody rb;
    private Vector3 inputDirection;
    private float currentSpeed;

    public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        // 애니메이션 이동 정보 무시 (Root Motion OFF)
        animator.applyRootMotion = false;
    }

    void Update()
    {
        // 이동 입력
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        inputDirection = new Vector3(x, 0, z).normalized;

        // 달리기 여부
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        currentSpeed = isRunning ? runSpeed : walkSpeed;

        // 애니메이션 이동 속도 설정
        float animSpeed = inputDirection.magnitude * (isRunning ? 1.0f : 0.5f);
        animator.SetFloat("Speed", animSpeed);
    }

    void FixedUpdate()
    {
        // 캐릭터 방향 기준으로 이동
        Vector3 moveDirection = transform.TransformDirection(inputDirection);
        Vector3 movement = moveDirection * currentSpeed * Time.fixedDeltaTime;

        // Rigidbody 이동 (물리 충돌 포함)
        rb.MovePosition(rb.position + movement);
    }

}
