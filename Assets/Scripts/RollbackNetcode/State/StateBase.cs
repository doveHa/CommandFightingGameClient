
namespace RollbackNetcode.State
{
    public abstract class StateBase
    {
        public abstract void Simulate(bool isLocal, float startTime);
        public abstract StateBase Clone();
    }
}