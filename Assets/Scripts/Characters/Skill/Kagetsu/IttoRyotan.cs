using Characters.AnimationHandler;
using UnityEngine;

namespace Characters.Skill.Kagetsu
{
    public class IttoRyotan : ICharacterSkill
    {
        public override int Damage { get; protected set; } = 24;
        
        private KagetsuAnimationHandler kagetsuAnimationHandler;

        public override void Run(float startTime)
        {
            Debug.Log("IttoRyotan");
            HasHit = false;
            kagetsuAnimationHandler = transform.parent.GetComponent<KagetsuAnimationHandler>();
            kagetsuAnimationHandler.StartIttoRyotanAnimation(startTime);
        }
    }
}