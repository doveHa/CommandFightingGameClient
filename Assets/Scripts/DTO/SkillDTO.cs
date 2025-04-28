using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DTO
{
    public class SkillDTO
    {
        [JsonPropertyName("strengthCoefficient")]
        public int AtkCoeff { get; set; }
        [JsonPropertyName("healthCoefficient")]
        public int HpCoeff { get; set; }
        [JsonPropertyName("moveSpeedCoefficient")]
        public int MoveSpeedCoeff { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("description")] 
        public string Description { get; set; }
        [JsonPropertyName("coolTime")]
        public int CoolTime { get; set; }
        [JsonPropertyName("defaultCommand")]
        public List<string> Command { get; set; }
    }
}