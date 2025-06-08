using Manager;
using Movement;
using UnityEngine;

namespace RollbackNetcode
{
    public class JumpState : State
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

        public override void Simulate(bool isLocal)
        {
            if (Jumped)
            {
                GameObject target =
                    isLocal ? VarManager.Manager.PlayerGameObject : VarManager.Manager.OpponentGameObject;
                CharacterMovementController.JumpCharacter(target);
            }
        }

        public override State Clone()
        {
            return new JumpState(Jumped);
        }
    }
}