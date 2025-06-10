using Characters.AnimationHandler;
using Manager;
using UnityEngine;

namespace Characters.Skill.Kagetsu
{
    public class Sangiri : ICharacterSkill
    {
        public override int Damage { get; protected set; } = 6;

        public void SetNigiri()
        {
            Damage = 6;
        }

        public void SetSangiri()
        {
            Damage = 8;
        }

        private KagetsuAnimationHandler kagetsuAnimationHandler;

        public override void Run(float startTime)
        {
            Debug.Log("Sangiri");

            HasHit = false;
            kagetsuAnimationHandler = transform.parent.GetComponent<KagetsuAnimationHandler>();
            kagetsuAnimationHandler.StartSangiriAnimation(startTime);
        }

        public override void Hit()
        {
        }
    }
}