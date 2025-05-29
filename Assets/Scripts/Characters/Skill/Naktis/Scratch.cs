using System.Collections;
using Characters.AnimationHandler;
using UnityEngine;
using Manager;

namespace Characters.Skill.Naktis
{
    public class Scratch : MonoBehaviour, ICharacterSkill
    {
        private NaktisAnimationHandler naktisAnimationHandler;

        public void SetCoff()
        {
        }

        public void Run()
        {
            naktisAnimationHandler = transform.parent.GetComponent<NaktisAnimationHandler>();
            naktisAnimationHandler.StartScratchAnimation();
            //StartCoroutine(WaitScratchTiming());
        }

        public void Hit()
        {
            VarManager.Manager.Opponent.Hit(10);
        }

        private IEnumerator WaitScratchTiming()
        {
            yield return new WaitUntil(() => naktisAnimationHandler.FirstScratch);
            FirstScratch();
            naktisAnimationHandler.FirstScratch = false;

            yield return new WaitUntil(() => naktisAnimationHandler.SecondScratch);
            SecondScratch();
            naktisAnimationHandler.SecondScratch = false;
        }

        private void FirstScratch()
        {
            Debug.Log(1);
        }

        private void SecondScratch()
        {
            Debug.Log(2);
        }
    }
}