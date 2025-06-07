using System;
using System.Collections.Generic;
using System.Linq;
using DTO;
using Manager;
using UnityEngine;

namespace Characters
{
    public class SkillGroup
    {
        public SkillGroup(Dictionary<string, Action<SkillInfo>> actionGroup, List<SkillDTO> skills)
        {
            Skills = new Dictionary<string, SkillInfo>();

            foreach (SkillDTO skill in skills)
            {
                //actionGroup.TryGetValue(skill.Name, out var action);
                Skills.Add(skill.Name, new SkillInfo(skill));
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
        public SkillInfo(SkillDTO skillDto)
        {
            Debug.Log(Name);
            Name = skillDto.Name;
            Description = skillDto.Description;
            Command = skillDto.Command;
            SkillIndex = VarManager.SkillMapping1(Name);
        }

        public int SkillIndex { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Command { get; set; }
        
    }
}