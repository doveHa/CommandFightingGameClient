using Characters.AnimationHandler;
using UnityEngine;
using Manager;

namespace Characters.Skill.Naktis
{
    public class UpperWing : MonoBehaviour, ICharacterSkill
    {
        private NaktisAnimationHandler naktisAnimationHandler;

        public void SetCoff()
        {
        }

        public void Run()
        {
            naktisAnimationHandler = transform.parent.GetComponent<NaktisAnimationHandler>();
            naktisAnimationHandler.StartUpperWingAnimation();
        }

        public void Hit()
        {
            VarManager.Manager.Opponent.Airborne(10);
        }
    }
}