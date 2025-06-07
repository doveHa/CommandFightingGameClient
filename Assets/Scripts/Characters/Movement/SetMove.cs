using Handler;
using Manager;
using RollbackNetcode;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Movement
{
    public class SetMove
    {
        private Vector2 moveDirection;

        public SetMove()
        {
            InputActionManager.Manager.Inputs.Inputs.Move.started += PerformKeyInput;
            InputActionManager.Manager.Inputs.Inputs.Move.canceled += CancelKeyInput;
        }

        public int MoveSet()
        {
            int playerMovement = PlayerMovement();

            RollbackManager.Manager.Move.PlayerStates.Add(RollbackManager.Manager.CurrentFrame,
                new MoveState(playerMovement));
            return playerMovement;
        }

        private void PerformKeyInput(InputAction.CallbackContext ctx)
        {
            VarManager.Manager.PlayerGameObject.GetComponent<CharacterAnimatorHandler>().StartWalkAnimation();
            moveDirection = ctx.ReadValue<Vector2>();
        }

        private void CancelKeyInput(InputAction.CallbackContext ctx)
        {
            moveDirection = Vector2.zero;
            VarManager.Manager.PlayerGameObject.GetComponent<CharacterAnimatorHandler>().EndWalkAnimation();
        }

        private int PlayerMovement()
        {
            if (moveDirection.x > 0)
            {
                return 1;
            }

            if (moveDirection.x < 0)
            {
                return -1;
            }

            return 0;
        }
    }
}