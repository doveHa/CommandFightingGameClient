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

        public override void Run()
        {
            HasHit = true;
            GetComponentInParent<CharacterAnimatorHandler>().StartJumpPunchAnimation();
            HasHit = false;
        }
    }
}