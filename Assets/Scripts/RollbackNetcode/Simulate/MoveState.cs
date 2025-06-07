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
        
        public override void Run(bool isPlayer)
        {
            GameObject target = isPlayer ? VarManager.Manager.PlayerGameObject : VarManager.Manager.OpponentGameObject;

            CharacterMovementController.MoveCharacter(target, MoveDirection);
        }
    }
}