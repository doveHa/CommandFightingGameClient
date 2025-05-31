using System.Collections;
using System.Collections.Generic;
using DataTable.FrameRanges;
using Handler;
using UnityEngine;

namespace Characters.AnimationHandler
{
    public class NaktisAnimationHandler : CharacterAnimatorHandler
    {
        //private Dictionary<string, bool> animationFlag;
        private bool motionFlag = false;
        private int upperWingLayerIndex, scratchLayerIndex, hasegiLayerIndex, flyLayerIndex;

        public bool ShootHasegi { get; set; }


        protected override void Awake()
        {
            base.Awake();
            dictionary = new NaktisFrameRangesDictionary();
            /*
            animationFlag = new Dictionary<string, bool>();
            animationFlag.Add("Hasegi", false);
            animationFlag.Add("Scratch", false);
            animationFlag.Add("UpperWing", false);
            */
            LayerIndexInitialize();
            ShootHasegi = false;
        }

        private void LayerIndexInitialize()
        {
            upperWingLayerIndex = Animator.GetLayerIndex("UpperWing");
            scratchLayerIndex = Animator.GetLayerIndex("Scratch");
            hasegiLayerIndex = Animator.GetLayerIndex("Hasegi");
            flyLayerIndex = Animator.GetLayerIndex("Fly");
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        public void StartScratchAnimation()
        {
            if (!motionFlag)
            {
                motionFlag = true;
                ChangeLayer(scratchLayerIndex);
                Animator.SetBool("ScratchExit", false);
                Animator.Play("Scratch", CurrentLayerIndex, 0);
            }
        }

        public void StartUpperWingAnimation()
        {
            if (!motionFlag)
            {
                motionFlag = true;
                ChangeLayer(upperWingLayerIndex);
                Animator.SetBool("UpperWingExit", false);
                Animator.Play("UpperWing", CurrentLayerIndex, 0);
            }
        }

        public void StartFlyAnimation()
        {
            if (!motionFlag)
            {
                motionFlag = true;
                ChangeLayer(flyLayerIndex);
                Animator.SetBool("FlyExit", false);
                Animator.Play("Fly", CurrentLayerIndex, 0);
            }
        }

        public void StartHasegiAnimation()
        {
            if (!motionFlag)
            {
                motionFlag = true;
                ChangeLayer(hasegiLayerIndex);
                Animator.SetBool("HasegiExit", false);
                Animator.Play("Hasegi", CurrentLayerIndex, 0);
            }
        }

        public void EndScratchAnimation()
        {
            FlagInitialize();
            ChangeLayer(baseLayerIndex);
            Animator.SetBool("ScratchExit", true);
            Animator.Play("Idle");
        }

        public void EndUpperWingAnimation()
        {
            FlagInitialize();
            ChangeLayer(baseLayerIndex);
            Animator.SetBool("UpperWingExit", true);
            Animator.Play("Idle");
        }

        public void EndFlyAnimation()
        {
            FlagInitialize();
            Animator.SetBool("IsFlying", false);
            foreach (AnimatorControllerParameter parameter in Animator.parameters)
            {
                if (parameter.type == AnimatorControllerParameterType.Trigger)
                {
                    Animator.ResetTrigger(parameter.name);
                    //animationFlag[parameter.name] = false;
                }
            }

            motionFlag = false;
        }

        public void EndHasegiAnimation()
        {
            FlagInitialize();
            ChangeLayer(baseLayerIndex);
            Animator.SetBool("HasegiExit", true);
            Animator.Play("Idle");
        }

        public void ShootingHasegi()
        {
            ShootHasegi = true;
        }

        private void FlagInitialize()
        {
            PunchFlagInitialize();
            motionFlag = false;
        }
    }
}