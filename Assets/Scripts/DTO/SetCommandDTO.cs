using System.Collections.Generic;

namespace DTO
{
    public class SetCommandDTO
    {
        public SetCommandDTO(string characterName, string skillName, List<string> command)
        {
            this.characterName = characterName;
            this.skillName = skillName;
            this.command = command;
        }

        public List<string> command { get; set; }
        public string characterName { get; set; }
        public string skillName { get; set; }
    }
}