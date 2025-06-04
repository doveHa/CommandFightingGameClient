using Handler;
using Manager;
using UnityEngine;

namespace Characters.Skill
{
    public class Punch : MonoBehaviour, ICharacterSkill
    {
        public bool HasHit { get; set; }

        void Start()
        {
            InputActionManager.Manager.Inputs.Inputs.BasicAtk.started += (ctx =>
            {
                if (VarManager.Manager.Player.IsJumping)
                {
                    VarManager.Manager.PlayerGameObject.GetComponent<SendKey>().SetSkillName("Jumping_Attack");
                }
                else
                {
                    VarManager.Manager.PlayerGameObject.GetComponent<SendKey>().SetSkillName("Atk_Punch");
                }
            });
        }


        public void Run()
        {
            CharacterAnimatorHandler animator = GetComponentInParent<CharacterAnimatorHandler>();

            if (VarManager.Manager.Player.IsJumping)
            {
                animator.StartJumpPunchAnimation();
            }
            else
            {
                animator.StartPunchAnimation();
            }

            HasHit = false;
        }

        public void Hit()
        {
            HasHit = true;
            VarManager.Manager.Opponent.Hit(10);
        }
    }
}