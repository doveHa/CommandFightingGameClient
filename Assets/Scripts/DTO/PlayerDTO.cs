namespace DTO
{
    public class PlayerDTO
    {
        public PlayerDTO(string name, int experiencePoint, int matchCount)
        {
            this.name = name;
            this.experiencePoint = experiencePoint;
            this.matchCount = matchCount;
        }

        public string name { get; }
        public int experiencePoint { get; }
        public int matchCount { get; }
    }
}