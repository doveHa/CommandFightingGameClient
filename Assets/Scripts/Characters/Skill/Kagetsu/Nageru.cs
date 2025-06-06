using Characters.AnimationHandler;
using Manager;
using UnityEngine;

namespace Characters.Skill.Kagetsu
{
    public class Nageru : ICharacterSkill
    {
        public override int Damage { get; protected set; } = 14;

        
        private KagetsuAnimationHandler kagetsuAnimationHandler;
        
        public override void Run()
        {
            Debug.Log("Nageru");

            HasHit = false;
            kagetsuAnimationHandler = transform.parent.GetComponent<KagetsuAnimationHandler>();
            kagetsuAnimationHandler.StartNageruAnimation();
        }
    }
}