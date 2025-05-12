using UnityEngine;

namespace Movement
{
    public class CharacterMovementController : MonoBehaviour
    {
        public static void MoveCharacter(GameObject gameObject, int moveDirection)
        {
            Rigidbody2D body = gameObject.transform.GetComponent<Rigidbody2D>();
            body.linearVelocityX = moveDirection * ConstController.Manager.MoveSpeed;
        }

        public static void JumpCharacter(GameObject gameObject)
        {
            Player player = gameObject.transform.GetComponent<Player>();

            if (!player.IsJumping)
            {
                player.IsJumping = true;
                Rigidbody2D body = gameObject.transform.GetComponentInChildren<Rigidbody2D>();
                body.AddForce(Vector2.up * ConstController.Manager.JumpForce, ForceMode2D.Impulse);
            }
        }
    }
}