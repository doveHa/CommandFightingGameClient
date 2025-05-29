using UnityEngine;

namespace Movement
{
    public class Ground : MonoBehaviour
    {
        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Opponent"))
            {
                collision.gameObject.GetComponentInParent<Player>().IsJumping = false;
                collision.gameObject.GetComponentInParent<Player>().IsAirborne = false;
            }
        }
    }
}