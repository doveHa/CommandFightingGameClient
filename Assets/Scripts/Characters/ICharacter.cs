using System.Collections.Generic;
using DTO;

namespace Characters
{
    public interface ICharacter
    {
        string CharacterName { get; set; }
        int Hp { get; set; }
        int Atk { get; set; }
        int MoveSpeed { get; set; }
        SkillGroup SkillGroup { get; set; }
        List<SetCommandDTO> CommandListSkills();
    }
}