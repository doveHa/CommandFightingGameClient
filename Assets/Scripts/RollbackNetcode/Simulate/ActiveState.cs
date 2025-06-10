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

        public override void Simulate(bool isLocal, float startTime)
        {
            if (SkillIndex == Constant.SkillName.NONE)
            {
                return;
            }

            if (isLocal)
            {
                VarManager.Manager.PlayerSkills[SkillIndex].Run(0);
            }
            else
            {
                VarManager.Manager.OpponentSkills[SkillIndex].Run(startTime);
            }
        }

        public override State Clone()
        {
            return new ActiveState(SkillIndex);
        }
    }
}