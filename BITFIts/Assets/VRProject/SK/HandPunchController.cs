using UnityEngine;

public class HandPunchController : MonoBehaviour
{
    private Animator animator;
    private HandGlowEffect glowEffect;

    public enum HandType { Left, Right }
    public HandType handType; // ���� ���� ���� (Inspector���� Left �Ǵ� Right��)

    void Start()
    {
        animator = GetComponent<Animator>();
        glowEffect = GetComponent<HandGlowEffect>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Block")) return;

        // ����� ���� ������ ������
        Block block = other.GetComponent<Block>();
        if (block == null) return;

        bool validHit =
            (block.blockColor == Block.BlockColorType.Red && handType == HandType.Right) ||
            (block.blockColor == Block.BlockColorType.Blue && handType == HandType.Left);

        if (validHit)
        {
            // �� �ִϸ��̼� + �۷ο� ����
            animator.SetTrigger("PunchTrigger");
            glowEffect.PlayGlow();
        }
    }
}
