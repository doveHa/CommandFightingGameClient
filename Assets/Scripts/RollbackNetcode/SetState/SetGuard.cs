using Manager;
using UnityEngine;
using UnityEngine.InputSystem;
using RollbackNetcode;
using RollbackNetcode.State;
using RollbackNetcode.StateSimulator;

namespace RollbackNetcode.SetState
{
    public class SetGuard : SetStateBase
    {
        private bool guardKeyInput = false;
        private bool prevInput = false;

        public SetGuard()
        {
            InputActionManager.Manager.Inputs.Inputs.Guard.performed += GuardKeyInput;
            InputActionManager.Manager.Inputs.Inputs.Guard.canceled += GuardKeyInputCancel;
        }

        public override void ApplyState()
        {
            if (guardKeyInput != prevInput)
            {
                SteamNetworkManager.Manager.SendMsg(SteamNetworkManager.Manager.RemoteSteamId,
                    Constant.SteamNetworkingType.KEYINPUT,
                    MessageFormatting(Constant.SteamNetworkingType.KeyInput.GUARDSTATE, StateSet()));
                Initialize();
            }
        }

        protected override string StateSet()
        {
            RollbackManager.Manager.GuardSimulatorBase.AddState(StateSimulatorBase.CurrentFrame,
                new GuardState(guardKeyInput));
            return guardKeyInput.ToString();
        }

        protected override void Initialize()
        {
            prevInput = guardKeyInput;
        }


        private void GuardKeyInput(InputAction.CallbackContext ctx)
        {
            guardKeyInput = true;
            RollbackManager.Manager.GuardSimulatorBase.AddState(StateSimulatorBase.CurrentFrame, new GuardState(guardKeyInput));
            Debug.Log("StartGuard");
            VarManager.Manager.Player.IsGuard = true;
        }

        private void GuardKeyInputCancel(InputAction.CallbackContext ctx)
        {
            guardKeyInput = false;
            RollbackManager.Manager.GuardSimulatorBase.AddState(StateSimulatorBase.CurrentFrame, new GuardState(guardKeyInput));
            VarManager.Manager.Player.IsGuard = false;
        }
    }
}