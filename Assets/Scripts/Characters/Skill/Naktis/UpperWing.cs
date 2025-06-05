using Characters.AnimationHandler;
using UnityEngine;
using Manager;

namespace Characters.Skill.Naktis
{
    public class UpperWing : ICharacterSkill
    {
        private NaktisAnimationHandler naktisAnimationHandler;

        protected override int Damage { get; set; } = 12;

        public override void Run()
        {
            HasHit = false;
            naktisAnimationHandler = transform.parent.GetComponent<NaktisAnimationHandler>();
            naktisAnimationHandler.StartUpperWingAnimation();
        }
    }
}