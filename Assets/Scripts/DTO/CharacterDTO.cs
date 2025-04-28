using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DTO
{
    public class CharacterDTO
    {
        [JsonPropertyName("name")]
        public string CharacterName { get; set; }
        [JsonPropertyName("health")]
        public int Hp { get; set; }
        [JsonPropertyName("strength")]
        public int Atk { get; set; }
        [JsonPropertyName("moveSpeed")]
        public int MoveSpeed { get; set; }
        [JsonPropertyName("skills")]
        public List<SkillDTO> Skill { get; set;}
    }
}