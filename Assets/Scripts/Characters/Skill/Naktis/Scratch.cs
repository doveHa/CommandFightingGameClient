using System.Collections;
using Characters.AnimationHandler;
using UnityEngine;
using Manager;

namespace Characters.Skill.Naktis
{
    public class Scratch : ICharacterSkill
    {
        private NaktisAnimationHandler naktisAnimationHandler;

        protected override int Damage { get; set; } = 7;
        private int Damage2 { get; set; } = 10;

        public void SetScratch1()
        {
            Damage = 7;
        }

        public void SetScratch2()
        {
            Damage = 10;
        }
        public override void Run()
        {
            HasHit = false;
            naktisAnimationHandler = transform.parent.GetComponent<NaktisAnimationHandler>();
            naktisAnimationHandler.StartScratchAnimation();
        }
    }
}