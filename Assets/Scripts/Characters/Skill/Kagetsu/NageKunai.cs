using System.Collections;
using Characters.AnimationHandler;
using Manager;
using UnityEngine;

namespace Characters.Skill.Kagetsu
{
    public class NageKunai : ICharacterSkill
    {
        protected override int Damage { get; set; } = 14;

        [SerializeField] private GameObject leftSideStartPosition, rightSideStartPosition;
        private KagetsuAnimationHandler kagetsuAnimationHandler;

        public override void Run()
        {
            Debug.Log("NageKunai");

            HasHit = false;
            kagetsuAnimationHandler = transform.parent.GetComponent<KagetsuAnimationHandler>();
            kagetsuAnimationHandler.StartNageKunaiAnimation();
            StartCoroutine(WaitKunaiMotion());
        }

        private IEnumerator WaitKunaiMotion()
        {
            yield return new WaitUntil(() => kagetsuAnimationHandler.ShootKunai);

            Vector2 startDirection;
            if (VarManager.Manager.Player.IsLeft)
            {
                startDirection = leftSideStartPosition.transform.position;
            }
            else
            {
                startDirection = rightSideStartPosition.transform.position;
            }

            CreateProjectile(startDirection, "Prefabs/Character/Kagetsu/Skill/Kunai");
            kagetsuAnimationHandler.ShootKunai = false;
        }
    }
}