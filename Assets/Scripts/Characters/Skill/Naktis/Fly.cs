using System.Collections;
using Characters;
using Characters.AnimationHandler;
using Unity.VisualScripting;
using UnityEngine;

namespace Characters.Skill.Naktis
{
    public class Fly : ICharacterSkill
    {
        private NaktisAnimationHandler naktisAnimationHandler;
        private Coroutine flyCoroutine;
        
        public override void Run()
        {
            naktisAnimationHandler = transform.parent.GetComponent<NaktisAnimationHandler>();

            if (!flyCoroutine.IsUnityNull())
            {
                naktisAnimationHandler.EndFlyAnimation();
                GetComponentInParent<Rigidbody2D>().gravityScale = ConstController.Manager.GravityScale;
                StopCoroutine(flyCoroutine);

                flyCoroutine = null;
            }
            else
            {
                flyCoroutine = StartCoroutine(NaktisFly());
                GetComponentInParent<Player>().IsJumping = true;
            }
        }
        

        private IEnumerator NaktisFly()
        {
            Rigidbody2D body = GetComponentInParent<Rigidbody2D>();
            ConstController.Manager.GravityScale = body.gravityScale;
            naktisAnimationHandler.StartFlyAnimation();
            body.gravityScale = 0;
            body.linearVelocityY = 0;
            body.AddForce(Vector2.up * ConstController.Manager.FlyForce, ForceMode2D.Impulse);
            yield return new WaitForSeconds(ConstController.Manager.WaitTime);
            body.AddForce(Vector2.down * ConstController.Manager.FlyForce, ForceMode2D.Impulse);

            float elapsedTime = 0;
            while (elapsedTime < ConstController.Manager.DurationTime)
            {
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            body.gravityScale = ConstController.Manager.GravityScale;
            naktisAnimationHandler.EndFlyAnimation();
            flyCoroutine = null;
        }

    }
}