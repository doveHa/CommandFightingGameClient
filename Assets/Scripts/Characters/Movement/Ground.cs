using UnityEngine;

public class Ground : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Opponent"))
        {
            Player player = collision.gameObject.GetComponentInParent<Player>();
            if (player.IsJumping)
            {
                player.Animator.EndJumpAnimation();
            }

            player.IsJumping = false;
        }
    }
}