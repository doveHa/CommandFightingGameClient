using Handler;
using Manager;
using Movement;
using UnityEngine;

namespace RollbackNetcode
{
    public class MoveState : State
    {
        public int MoveDirection { private get; set; }

        public MoveState()
        {
            MoveDirection = 0;
        }

        public MoveState(int moveDirection)
        {
            MoveDirection = moveDirection;
        }

        public override void Simulate(bool isLocal, float startTime)
        {
            GameObject target = isLocal ? VarManager.Manager.PlayerGameObject : VarManager.Manager.OpponentGameObject;

            //Debug.Log($"Move State's MoveDirection {MoveDirection}");
            CharacterMovementController.MoveCharacter(target, MoveDirection);
        }

        public override void Print()
        {
            Debug.Log(MoveDirection);
        }

        public override State Clone()
        {
            return new MoveState(MoveDirection);
        }
    }
}