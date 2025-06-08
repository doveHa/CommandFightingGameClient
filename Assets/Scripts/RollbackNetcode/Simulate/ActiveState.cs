using Manager;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

namespace RollbackNetcode
{
    public class ActiveState : State
    {
        public int SkillIndex { private get; set; }

        public ActiveState()
        {
            SkillIndex = Constant.SkillName.NONE;
        }

        public ActiveState(int skillIndex)
        {
            SkillIndex = skillIndex;
        }

        public override void Run(bool isLocal)
        {
            if (SkillIndex == -1)
            {
                return;
            }

            if (isLocal)
            {
                VarManager.Manager.PlayerSkills[SkillIndex].Run();
            }
            else
            {
                VarManager.Manager.OpponentSkills[SkillIndex].Run();
            }
        }

        public override State Clone()
        {
            return new ActiveState(SkillIndex);
        }
    }
}