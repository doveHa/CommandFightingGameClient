using Handler;
using Manager;
using UnityEngine;

namespace Characters.Skill
{
    public class Punch : ICharacterSkill
    {
        public void JumpPunch()
        {
            Damage = 8;
        }

        void Start()
        {
            Damage = 5;
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

        public override void Run()
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
    }
}