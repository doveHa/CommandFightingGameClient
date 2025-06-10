using Characters.AnimationHandler;
using UnityEngine;
using Manager;

namespace Characters.Skill.Naktis
{
    public class UpperWing : ICharacterSkill
    {
        private NaktisAnimationHandler naktisAnimationHandler;

        public override int Damage { get; protected set; } = 12;

        public override void Run(float startTime)
        {
            HasHit = false;
            naktisAnimationHandler = transform.parent.GetComponent<NaktisAnimationHandler>();
            naktisAnimationHandler.StartUpperWingAnimation(startTime);
        }
    }
}