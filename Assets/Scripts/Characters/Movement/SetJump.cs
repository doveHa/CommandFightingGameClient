using System;
using Handler;
using Manager;
using RollbackNetcode;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Movement
{
    public class SetJump
    {
        private bool jumpKeyInput = false;

        public SetJump()
        {
            InputActionManager.Manager.Inputs.Inputs.Jump.performed += JumpKeyInput;
        }

        public bool JumpSet()
        {
            RollbackManager.Manager.LocalSimulator.JumpStates.Add(RollbackManager.Manager.CurrentFrame,
                new JumpState(jumpKeyInput));
            return jumpKeyInput;
        }

        public void Initialize()
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