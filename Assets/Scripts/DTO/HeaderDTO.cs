namespace DTO
{
    public class HeaderDTO
    {
        public HeaderDTO(string name, string value)
        {
            this.name = name;
            this.value = value;
        }
        public string name { get; }
        public string value { get; }
    }
}