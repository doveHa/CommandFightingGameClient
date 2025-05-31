using System.Collections;
using Characters.AnimationHandler;
using UnityEngine;
using Manager;

namespace Characters.Skill.Naktis
{
    public class Scratch : MonoBehaviour, ICharacterSkill
    {
        public bool HasHit { get; set; }

        private NaktisAnimationHandler naktisAnimationHandler;

        public void SetCoff()
        {
        }

        public void Run()
        {
            HasHit = false;
            naktisAnimationHandler = transform.parent.GetComponent<NaktisAnimationHandler>();
            naktisAnimationHandler.StartScratchAnimation();
        }

        public void Hit()
        {
            HasHit = true;
            VarManager.Manager.Opponent.Hit(10);
        }
    }
}