using System.Collections;
using Characters.AnimationHandler;
using Manager;
using UnityEngine;

namespace Characters.Skill.Naktis
{
    public class Hasegi : ICharacterSkill
    {
        public override int Damage { get; protected set; } = 14;
        
        [SerializeField] private GameObject leftSideStartPosition, rightSideStartPosition;
        private NaktisAnimationHandler naktisAnimationHandler;

        public override void Run(float startTime)
        {
            HasHit = false;
            naktisAnimationHandler = transform.parent.GetComponent<NaktisAnimationHandler>();
            naktisAnimationHandler.StartHasegiAnimation(startTime);
            StartCoroutine(WaitHasegiMotion());
        }

        private IEnumerator WaitHasegiMotion()
        {
            yield return new WaitUntil(() => naktisAnimationHandler.ShootHasegi);

            Vector2 startDirection;
            if (GetComponentInParent<Player>().IsLeft)
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