using Handler;
using Manager;
using UnityEngine;

namespace Characters.Skill
{
    public class JumpPunch : ICharacterSkill
    {
        public override int Damage { get; protected set; } = 5;

        void Start()
        {
            SetCommandCoff(1);
        }

        public override void Run(float startTime)
        {
            HasHit = true;
            GetComponentInParent<CharacterAnimatorHandler>().StartJumpPunchAnimation(startTime);
            HasHit = false;
        }
    }
}