using Characters.AnimationHandler;
using Manager;
using UnityEngine;

namespace Characters.Skill.Kagetsu
{
    public class NageKunai : ICharacterSkill
    {
        protected override int Damage { get; set; } = 14;
        
        private KagetsuAnimationHandler kagetsuAnimationHandler;

        public override void Run()
        {
            Debug.Log("NageKunai");

            HasHit = false;
            kagetsuAnimationHandler = transform.parent.GetComponent<KagetsuAnimationHandler>();
            kagetsuAnimationHandler.StartNageKunaiAnimation();
        }
    }
}