using System.Collections;
using System.Collections.Generic;
using DataTable.FrameRanges;
using Handler;
using UnityEngine;

namespace Characters.AnimationHandler
{
    public class KagetsuAnimationHandler : CharacterAnimatorHandler
    {
        private int nageruLayerIndex, sangiriLayerIndex, nageKunaiLayerIndex, ittoRyotanLayerIndex;

        public bool ShootKunai { get; set; }

        protected override void Awake()
        {
            base.Awake();
            dictionary = new KagetsuFrameRangesDictionary();
            LayerIndexInitialize();
            ShootKunai = false;
        }

        private void LayerIndexInitialize()
        {
            nageruLayerIndex = Animator.GetLayerIndex("Nageru");
            sangiriLayerIndex = Animator.GetLayerIndex("Sangiri");
            nageKunaiLayerIndex = Animator.GetLayerIndex("NageKunai");
            ittoRyotanLayerIndex = Animator.GetLayerIndex("IttoRyotan");
        }

        public void StartNageruAnimation(float startTime)
        {
            if (!motionFlag)
            {
                LockMovement();
                motionFlag = true;
                ChangeLayer(nageruLayerIndex);
                Animator.SetBool("NageruExit", false);
                Animator.Play("잡기", nageruLayerIndex, startTime);
            }
        }

        public void StartSangiriAnimation(float startTime)
        {
            if (!motionFlag)
            {
                LockMovement();
                motionFlag = true;
                ChangeLayer(sangiriLayerIndex);
                Animator.SetBool("SangiriExit", false);
                Animator.Play("3단 베기", sangiriLayerIndex, startTime);
            }
        }

        public void StartNageKunaiAnimation(float startTime)
        {
            if (!motionFlag)
            {
                LockMovement();
                motionFlag = true;
                ChangeLayer(nageKunaiLayerIndex);
                Animator.SetBool("NageKunaiExit", false);
                Animator.Play("쿠나이", nageKunaiLayerIndex, startTime);
            }
        }

        public void StartIttoRyotanAnimation(float startTime)
        {
            if (!motionFlag)
            {
                LockMovement();
                motionFlag = true;
                ChangeLayer(ittoRyotanLayerIndex);
                Animator.SetBool("IttoRyotanExit", false);
                Animator.Play("베기", ittoRyotanLayerIndex, startTime);
            }
        }

        public void EndNageruAnimation()
        {
            UnLockMovement();
            FlagInitialize();
            ChangeLayer(baseLayerIndex);
            Animator.SetBool("NageruExit", true);
            Animator.Play("Idle");
        }

        public void EndSangiriAnimation()
        {
            UnLockMovement();
            FlagInitialize();
            ChangeLayer(baseLayerIndex);
            Animator.SetBool("SangiriExit", true);
            Animator.Play("Idle");
        }

        public void EndNageKunaiAnimation()
        {
            UnLockMovement();
            FlagInitialize();
            ChangeLayer(baseLayerIndex);
            Animator.SetBool("NageKunaiExit", true);
            Animator.Play("Idle");
        }

        public void EndIttoRyotanAnimation()
        {
            UnLockMovement();
            FlagInitialize();
            ChangeLayer(baseLayerIndex);
            Animator.SetBool("IttoRyotanExit", true);
            Animator.Play("Idle");
        }
        
        protected override void FlagInitialize()
        {
            PunchFlagInitialize();
            motionFlag = false;
        }
    }
}