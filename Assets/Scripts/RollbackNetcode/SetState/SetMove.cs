using Handler;
using Manager;
using UnityEngine;
using UnityEngine.InputSystem;
using RollbackNetcode;
using RollbackNetcode.State;
using RollbackNetcode.StateSimulator;

namespace RollbackNetcode.SetState
{
    public class SetMove : SetStateBase
    {
        private Vector2 moveDirection;
        private int prevMove;

        public SetMove()
        {
            InputActionManager.Manager.Inputs.Inputs.Move.started += PerformKeyInput;
            InputActionManager.Manager.Inputs.Inputs.Move.canceled += CancelKeyInput;
        }

        public override void ApplyState()
        {
            if (PlayerMovement() != prevMove)
            {
                SteamNetworkManager.Manager.SendMsg(SteamNetworkManager.Manager.RemoteSteamId,
                    Constant.SteamNetworkingType.KEYINPUT,
                    MessageFormatting(Constant.SteamNetworkingType.KeyInput.MOVESTATE, StateSet()));
                Initialize();
            }
        }

        protected override string StateSet()
        {
            int playerMovement = PlayerMovement();

            RollbackManager.Manager.MoveStateSimulatorBase.AddState(StateSimulatorBase.CurrentFrame,
                new MoveState(playerMovement));

            return (-1 * playerMovement).ToString();
        }

        protected override void Initialize()
        {
            prevMove = PlayerMovement();
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