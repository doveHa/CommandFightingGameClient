using System.IO.Enumeration;
using Manager;
using RollbackNetcode;
using RollbackNetcode.State;
using RollbackNetcode.StateSimulator;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RollbackNetcode.SetState
{
    public class SetActive : SetStateBase
    {
        public static int SkillIndex { get; set; } = Constant.SkillName.NONE;

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
            RollbackManager.Manager.ActiveSimulatorBase.AddState(StateSimulatorBase.CurrentFrame,
                new ActiveState(SkillIndex));
            return SkillIndex.ToString();
        }

        protected override void Initialize()
        {
            SkillIndex = Constant.SkillName.NONE;
        }


        private void PunchKeyInput(InputAction.CallbackContext context)
        {
            if (VarManager.Manager.Player == null)
            {
                GameObject.Find("Character").GetComponent<UserMainSkill>().Punch();
                return;
            }

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