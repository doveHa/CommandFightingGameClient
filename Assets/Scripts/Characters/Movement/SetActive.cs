using Manager;
using RollbackNetcode;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Movement
{
    public class SetActive
    {
        public static int SkillIndex { private get; set; } = Constant.SkillName.NONE;

        public SetActive()
        {
            InputActionManager.Manager.Inputs.Inputs.Guard.performed += GuardKeyInput;
            InputActionManager.Manager.Inputs.Inputs.Guard.canceled += GuardKeyInputCancel;
            InputActionManager.Manager.Inputs.Inputs.BasicAtk.started += PunchKeyInput;
        }

        public int ActiveSet()
        {
            RollbackManager.Manager.LocalSimulator.ActiveStates.Add(RollbackManager.Manager.CurrentFrame,
                new ActiveState(SkillIndex));

            return SkillIndex;
        }

        public void Initialize()
        {
            SkillIndex = Constant.SkillName.NONE;
        }

        private void PunchKeyInput(InputAction.CallbackContext context)
        {
            if (VarManager.Manager.Player.IsJumping)
            {
                SkillIndex = Constant.SkillName.JUMP_PUNCH;
            }
            else
            {
                SkillIndex = Constant.SkillName.PUNCH;
            }
        }

        private void GuardKeyInput(InputAction.CallbackContext ctx)
        {
            VarManager.Manager.Player.IsGuard = true;
        }

        private void GuardKeyInputCancel(InputAction.CallbackContext ctx)
        {
            VarManager.Manager.Player.IsGuard = false;
        }
    }
}