using Manager;
using Movement;
using UnityEngine;

namespace RollbackNetcode.State
{
    public class JumpState : StateBase
    {
        public bool Jumped { private get; set; }

        public JumpState()
        {
            Jumped = false;
        }

        public JumpState(bool jumped)
        {
            Jumped = jumped;
        }

        public override void Simulate(bool isLocal, float startTime)
        {
            if (Jumped)
            {
                GameObject target =
                    isLocal ? VarManager.Manager.PlayerGameObject : VarManager.Manager.OpponentGameObject;
                CharacterMovementController.JumpCharacter(target);
            }
        }

        public override StateBase Clone()
        {
            return new JumpState(Jumped);
        }
    }
}