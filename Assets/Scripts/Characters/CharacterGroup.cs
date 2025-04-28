using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DTO;
using UnityEngine;

namespace Characters
{
    public class CharacterGroup
    {
        public Dictionary<string, ICharacter> Characters { get; }

        public Dictionary<string, List<string>> CurrentCommandList { get; set; }
//        public List<SetCommandDTO> CurrentCommandList { get; set; }

        public CharacterGroup()
        {
            Characters = new Dictionary<string, ICharacter>();
        }


        public void Add(CharacterDTO characterDto)
        {
            Characters.Add(characterDto.CharacterName, new Character(characterDto));
        }

        public void ChangeCommand(string characterName, string skillName, List<string> command)
        {
            Characters.TryGetValue(characterName, out ICharacter character);
            character.SkillGroup.ChangeCommand(skillName, command);
        }

        public Dictionary<string, List<string>> AllCommands()
        {
            Dictionary<string, List<string>> commands = new Dictionary<string, List<string>>();

            foreach (KeyValuePair<string, ICharacter> character in Characters)
            {
                string characterName = character.Key;
                foreach (SetCommandDTO skill in character.Value.CommandListSkills())
                {
                    commands.Add(characterName + "+" + skill.skillName, skill.command);
                }
            }

            return commands;
        }

        public void InitializeCurrentCommandList()
        {
            CurrentCommandList = AllCommands();
        }
    }

    public class Character : ICharacter
    {
        private List<SetCommandDTO> customCommandListSkills;

        public Character(CharacterDTO characterDto)
        {
            CharacterName = characterDto.CharacterName;
            Hp = characterDto.Hp;
            Atk = characterDto.Atk;
            MoveSpeed = characterDto.MoveSpeed;
            SkillMethodGroup.actions.TryGetValue(CharacterName, out var actionGroup);
            SkillGroup = new SkillGroup(actionGroup, characterDto.Skill);
        }

        public string CharacterName { get; set; }
        public int Hp { get; set; }
        public int Atk { get; set; }
        public int MoveSpeed { get; set; }
        public SkillGroup SkillGroup { get; set; }

        public List<SetCommandDTO> CommandListSkills()
        {
            return SkillGroup.CustomCommandLists(CharacterName);
        }
    }
}