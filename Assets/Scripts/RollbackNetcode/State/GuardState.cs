namespace RollbackNetcode.State
{
    public class GuardState : StateBase
    {
        public bool Guarded { private get; set; }

        public GuardState()
        {
            Guarded = false;
        }

        public GuardState(bool guarded)
        {
            Guarded = guarded;
        }

        public override void Simulate(bool isLocal, float startTime)
        {
            Player target = isLocal ? VarManager.Manager.Player : VarManager.Manager.Opponent;

            if (Guarded)
            {
                target.Animator.StartGuardAnimation();
            }
            else
            {
                target.Animator.EndGuardAnimation();
            }
        }

        public override StateBase Clone()
        {
            return new GuardState(Guarded);
        }
    }
}