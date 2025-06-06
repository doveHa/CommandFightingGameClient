using System.Collections;
using Characters.AnimationHandler;
using Manager;
using UnityEngine;

namespace Characters.Skill.Naktis
{
    public class Hasegi : ICharacterSkill
    {
        protected override int Damage { get; set; } = 14;
        
        [SerializeField] private GameObject leftSideStartPosition, rightSideStartPosition;
        private NaktisAnimationHandler naktisAnimationHandler;

        public override void Run()
        {
            HasHit = false;
            naktisAnimationHandler = transform.parent.GetComponent<NaktisAnimationHandler>();
            naktisAnimationHandler.StartHasegiAnimation();
            StartCoroutine(WaitHasegiMotion());
        }

        private IEnumerator WaitHasegiMotion()
        {
            yield return new WaitUntil(() => naktisAnimationHandler.ShootHasegi);

            Vector2 startDirection;
            if (VarManager.Manager.Player.IsLeft)
            {
                startDirection = leftSideStartPosition.transform.position;
            }
            else
            {
                startDirection = rightSideStartPosition.transform.position;
            }
            CreateProjectile(startDirection, "Prefabs/Character/Naktis/Skill/Hasegi");
            naktisAnimationHandler.ShootHasegi = false;
        }
    }
}