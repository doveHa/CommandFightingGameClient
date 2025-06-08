using System;
using System.Text;
using Handler;
using Manager;
using RollbackNetcode;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Movement
{
    public class SetJump : SetState
    {
        private bool jumpKeyInput = false;
        public SetJump()
        {
            InputActionManager.Manager.Inputs.Inputs.Jump.performed += JumpKeyInput;
        }

        public override void ApplyState()
        {
            if (jumpKeyInput)
            {
                SteamNetworkManager.Manager.SendMsg(SteamNetworkManager.Manager.RemoteSteamId,
                    Constant.SteamNetworkingType.KEYINPUT,
                    MessageFormatting(Constant.SteamNetworkingType.KeyInput.JUMPSTATE, StateSet()));
                Initialize();
            }
        }

        protected override string StateSet()
        {
            RollbackManager.Manager.JumpStateSimulator.AddState(StateSimulator.CurrentFrame,
                new JumpState(jumpKeyInput));
            return jumpKeyInput.ToString();
        }

        protected override void Initialize()
        {
            jumpKeyInput = false;
        }

        private void JumpKeyInput(InputAction.CallbackContext ctx)
        {
            if (!VarManager.Manager.Player.IsJumping &&
                VarManager.Manager.PlayerGameObject.GetComponent<CharacterAnimatorHandler>().StartJumpAnimation())
            {
                jumpKeyInput = true;
            }
        }
    }
}