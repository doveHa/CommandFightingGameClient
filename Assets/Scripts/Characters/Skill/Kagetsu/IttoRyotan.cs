using Characters.AnimationHandler;
using Manager;
using UnityEngine;

namespace Characters.Skill.Kagetsu
{
    public class IttoRyotan : MonoBehaviour, ICharacterSkill
    {
        private KagetsuAnimationHandler kagetsuAnimationHandler;
        
        public void SetCoff()
        {
        }

        public void Run()
        {
            Debug.Log("IttoRyotan");
            HasHit = false;
            kagetsuAnimationHandler = transform.parent.GetComponent<KagetsuAnimationHandler>();
            kagetsuAnimationHandler.StartIttoRyotanAnimation();
        }

        public void Hit()
        {
            HasHit = true;
            VarManager.Manager.Opponent.Hit(10);
        }

        public bool HasHit { get; set; }

    }
}