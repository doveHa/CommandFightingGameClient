using Handler;
using Manager;
using UnityEngine;

namespace Characters.Skill
{
    public class Punch : MonoBehaviour, ICharacterSkill
    {
        void Start()
        {
            CharacterAnimatorHandler animator = GetComponentInParent<CharacterAnimatorHandler>();
            InputActionManager.Manager.Inputs.Atk.Atk.started += (ctx => { animator.StartPunchAnimation(); });
        }

        public void Run()
        {
        }

        public void Hit()
        {
            VarManager.Manager.Opponent.Hit(10);
        }
    }
}