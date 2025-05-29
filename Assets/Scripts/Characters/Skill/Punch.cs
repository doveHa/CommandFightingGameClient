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
            InputActionManager.Manager.Inputs.Atk.Atk.started += (ctx => { Run(); });
        }

        public void Run()
        {
            HasHit = false;
            CharacterAnimatorHandler animator = GetComponentInParent<CharacterAnimatorHandler>();
            animator.StartPunchAnimation();
        }

        public void Hit()
        {
            HasHit = true;
            VarManager.Manager.Opponent.Hit(10);
        }
    }
}