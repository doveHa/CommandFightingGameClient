using System.Collections.Generic;

namespace DTO
{
    public class GetCommandDTO
    {
        public GetCommandDTO(List<string> command, string characterName, string skillName)
        {
            this.command = command;
            this.characterName = characterName;
            this.skillName = skillName;
        }
        public List<string> command { get; set; }
        public string characterName { get; set; }
        public string skillName { get; set; }
    }
}