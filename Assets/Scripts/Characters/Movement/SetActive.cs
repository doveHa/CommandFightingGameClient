using System.Text;
using Manager;
using RollbackNetcode;
using UnityEngine;
using UnityEngine.InputSystem;
using ActiveState = RollbackNetcode.ActiveState;

namespace Movement
{
    public class SetActive : SetState
    {
        public static int SkillIndex { private get; set; } = Constant.SkillName.NONE;

        public SetActive()
        {
            InputActionManager.Manager.Inputs.Inputs.BasicAtk.started += PunchKeyInput;
        }

        public override void ApplyState()
        {
            if (SkillIndex != Constant.SkillName.NONE)
            {
                SteamNetworkManager.Manager.SendMsg(SteamNetworkManager.Manager.RemoteSteamId,
                    Constant.SteamNetworkingType.KEYINPUT,
                    MessageFormatting(Constant.SteamNetworkingType.KeyInput.ACTIVESTATE, StateSet()));
                Initialize();
            }
        }

        protected override string StateSet()
        {
            RollbackManager.Manager.ActiveSimulator.AddState(StateSimulator.CurrentFrame, new ActiveState(SkillIndex));
            return SkillIndex.ToString();
        }

        protected override void Initialize()
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
    }
}