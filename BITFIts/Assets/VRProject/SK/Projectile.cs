using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    public enum ProjectileColorType { Red, Blue }
    public ProjectileColorType projectileColor;

    public float speed = 10f;
    private Rigidbody rb;

    void Start()
    {

        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.up * speed, ForceMode.VelocityChange);
    }


    void OnTriggerEnter(Collider other)
    {
        // "Block" 태그를 가진 오브젝트와만 상호작용
        if (!other.CompareTag("Block")) return;

        Block block = other.GetComponent<Block>();
        if (block == null) return;

        bool isMatchingColor =
            (projectileColor == ProjectileColorType.Red && block.blockColor == Block.BlockColorType.Red) ||
            (projectileColor == ProjectileColorType.Blue && block.blockColor == Block.BlockColorType.Blue);

        if (isMatchingColor)
        {
            // 선택적으로 이펙트, 점수, 사운드 등을 넣을 수 있음
            Destroy(other.gameObject); // 블럭 제거
            Destroy(gameObject);       // 투사체 제거
        }
    }
}
