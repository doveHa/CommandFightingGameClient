using System;
using System.Collections.Generic;
using System.Linq;
using DTO;

namespace Characters
{
    public class SkillGroup
    {
        public SkillGroup(Dictionary<string, Action<SkillInfo>> actionGroup, List<SkillDTO> skills)
        {
            Skills = new Dictionary<string, SkillInfo>();

            foreach (SkillDTO skill in skills)
            {
                actionGroup.TryGetValue(skill.Name, out var action);
                Skills.Add(skill.Name, new SkillInfo(action, skill));
            }
        }

        public Dictionary<string, SkillInfo> Skills { get; }

        public void ChangeCommand(string skillName, List<string> command)
        {
            Skills.TryGetValue(skillName, out var skill);
            skill.Command = command.ToList();
        }

        public List<SetCommandDTO> CustomCommandLists(string characterName)
        {
            List<SetCommandDTO> skills = new List<SetCommandDTO>();
            foreach (KeyValuePair<string, SkillInfo> pair in Skills)
            {
                skills.Add(new SetCommandDTO(characterName, pair.Value.Name, pair.Value.Command));
            }

            return skills;
        }
    }

    public class SkillInfo
    {
        public SkillInfo(Action<SkillInfo> action, SkillDTO skillDto)
        {
            AtkCoeff = skillDto.AtkCoeff;
            HpCoeff = skillDto.HpCoeff;
            MoveSpeedCoeff = skillDto.MoveSpeedCoeff;
            Name = skillDto.Name;
            Description = skillDto.Description;
            CoolTime = skillDto.CoolTime;
            Command = skillDto.Command;
            Action = action;
        }

        public int AtkCoeff { get; set; }
        public int HpCoeff { get; set; }
        public int MoveSpeedCoeff { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CoolTime { get; set; }
        public List<string> Command { get; set; }

        public Action<SkillInfo> Action { get; set; }
    }
}