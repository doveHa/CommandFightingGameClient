using Characters.AnimationHandler;
using Manager;
using UnityEngine;

namespace Characters.Skill.Kagetsu
{
    public class Nageru : MonoBehaviour,ICharacterSkill
    {
        private KagetsuAnimationHandler kagetsuAnimationHandler;
        
        public void SetCoff()
        {
        }
        
        public void Run()
        {
            Debug.Log("Nageru");

            HasHit = false;
            kagetsuAnimationHandler = transform.parent.GetComponent<KagetsuAnimationHandler>();
            kagetsuAnimationHandler.StartNageruAnimation();
        }

        public void Hit()
        {
            HasHit = true;
            VarManager.Manager.Opponent.Hit(10);
        }

        public bool HasHit { get; set; }
    }
}