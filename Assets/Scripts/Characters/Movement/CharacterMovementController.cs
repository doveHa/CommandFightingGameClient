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
            gameObject.GetComponentInParent<Player>().Animator.StartJumpAnimation();
            gameObject.transform.GetComponentInParent<Player>().IsJumping = true;
            gameObject.transform.GetComponent<Rigidbody2D>()
                .AddForce(Vector2.up * ConstController.Manager.JumpForce, ForceMode2D.Impulse);
        }
    }
}