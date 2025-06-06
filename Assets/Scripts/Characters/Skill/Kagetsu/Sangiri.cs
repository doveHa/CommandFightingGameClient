using Characters.AnimationHandler;
using Manager;
using UnityEngine;

namespace Characters.Skill.Kagetsu
{
    public class Sangiri : ICharacterSkill
    {
        protected override int Damage { get; set; } = 6;
        private int Damage2 { get; set; } = 6;
        private int Damage3 { get; set; } = 8;

        private KagetsuAnimationHandler kagetsuAnimationHandler;

        public override void Run()
        {
            Debug.Log("Sangiri");

            HasHit = false;
            kagetsuAnimationHandler = transform.parent.GetComponent<KagetsuAnimationHandler>();
            kagetsuAnimationHandler.StartSangiriAnimation();
        }

        public override void Hit()
        {
        }
    }
}