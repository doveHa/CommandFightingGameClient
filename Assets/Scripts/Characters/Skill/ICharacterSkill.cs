public interface ICharacterSkill
{
    public void Run();
    public void Hit();
    bool HasHit { get; set; }
}