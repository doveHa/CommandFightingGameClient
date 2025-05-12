using System.Collections.Generic;
using Data.FrameRanges;
using DataTable;
using UnityEngine;

namespace Handler
{
    public abstract class CharacterAnimatorHandler : MonoBehaviour
    {
        protected Animator Animator;
        protected Transform PlayerTransform;
        
        private Dictionary<string, bool> animationFlag;

        protected virtual void Start()
        {
            Animator = GetComponent<Animator>();
            PlayerTransform = transform;
            
            animationFlag = new Dictionary<string, bool>();
            animationFlag.Add("Punch",false);
        }

        protected virtual void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartPunchAnimation();
            }
        }

        public void StartPunchAnimation()
        {
            if (!animationFlag["Punch"])
            {
                animationFlag["Punch"] = true;
                Animator.SetTrigger("Punch");
            }
        }

        public void FlagPunchFalse()
        {
            animationFlag["Punch"] = false;
        }

        public void StartWalkAnimation()
        {
            Animator.SetBool("IsMove", true);
        }

        public void EndWalkAnimation()
        {
            Animator.SetBool("IsMove", false);
        }
    }
}