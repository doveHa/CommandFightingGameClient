using Manager;
#if UNITY_EDITOR
using UnityEditor.Rendering;
#endif
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
                VarManager.Manager.OpponentSkills[SkillIndex].Run(0);
            }
        }

        public override State Clone()
        {
            return new ActiveState(SkillIndex);
        }
    }
}