using UnityEngine;

public class HandPunchController : MonoBehaviour
{
    private Animator animator;
    private HandGlowEffect glowEffect;

    public enum HandType { Left, Right }
    public HandType handType; // 손의 종류 설정 (Inspector에서 Left 또는 Right로)

    void Start()
    {
        animator = GetComponent<Animator>();
        glowEffect = GetComponent<HandGlowEffect>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Block")) return;

        // 블록의 색상 정보를 가져옴
        Block block = other.GetComponent<Block>();
        if (block == null) return;

        bool validHit =
            (block.blockColor == Block.BlockColorType.Red && handType == HandType.Right) ||
            (block.blockColor == Block.BlockColorType.Blue && handType == HandType.Left);

        if (validHit)
        {
            // 손 애니메이션 + 글로우 실행
            animator.SetTrigger("PunchTrigger");
            glowEffect.PlayGlow();
        }
    }
}
