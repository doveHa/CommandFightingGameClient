using RollbackNetCode;
using UnityEngine;

namespace Movement
{
    public class Ground : MonoBehaviour
    {
        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponentInParent<Player>().IsJumping = false;
            }
        }
    }
}