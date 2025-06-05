using Characters.AnimationHandler;
using UnityEngine;

namespace Characters.Skill.Kagetsu
{
    public class IttoRyotan : ICharacterSkill
    {
        protected override int Damage { get; set; } = 24;
        
        private KagetsuAnimationHandler kagetsuAnimationHandler;

        public override void Run()
        {
            Debug.Log("IttoRyotan");
            HasHit = false;
            kagetsuAnimationHandler = transform.parent.GetComponent<KagetsuAnimationHandler>();
            kagetsuAnimationHandler.StartIttoRyotanAnimation();
        }
    }
}