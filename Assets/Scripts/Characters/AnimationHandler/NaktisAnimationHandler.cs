using Characters.Skill.Naktis;
using DataTable.FrameRanges;
using Handler;

namespace Characters.AnimationHandler
{
    public class NaktisAnimationHandler : CharacterAnimatorHandler
    {
        private int upperWingLayerIndex, scratchLayerIndex, hasegiLayerIndex, flyLayerIndex;

        public bool ShootHasegi { get; set; }

        protected override void Awake()
        {
            base.Awake();
            dictionary = new NaktisFrameRangesDictionary();
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

        public void StartScratchAnimation()
        {
            if (!motionFlag)
            {
                LockMovement();
                motionFlag = true;
                ChangeLayer(scratchLayerIndex);
                Animator.SetBool("ScratchExit", false);
                Animator.Play("Scratch", scratchLayerIndex, 0);
            }
        }

        public void Scratch1Hit()
        {
            GetComponentInChildren<Scratch>().SetScratch1();
        }

        public void Scratch2Hit()
        {
            GetComponentInChildren<Scratch>().SetScratch2();
        }

        public void StartUpperWingAnimation()
        {
            if (!motionFlag)
            {
                LockMovement();
                motionFlag = true;
                ChangeLayer(upperWingLayerIndex);
                Animator.SetBool("UpperWingExit", false);
                Animator.Play("UpperWing", upperWingLayerIndex, 0);
            }
        }

        public void StartFlyAnimation()
        {
            if (!motionFlag)
            {
                motionFlag = true;
                ChangeLayer(flyLayerIndex);
                Animator.SetBool("FlyExit", false);
                Animator.Play("Fly_Up", flyLayerIndex, 0);
            }
        }

        public void StartHasegiAnimation()
        {
            if (!motionFlag)
            {
                LockMovement();
                motionFlag = true;
                ChangeLayer(hasegiLayerIndex);
                Animator.SetBool("HasegiExit", false);
                Animator.Play("Hasegi", hasegiLayerIndex, 0);
            }
        }

        public void EndScratchAnimation()
        {
            UnLockMovement();
            FlagInitialize();
            ChangeLayer(baseLayerIndex);
            EndAllAnimations();
            Animator.Play("Idle");
        }

        public void EndUpperWingAnimation()
        {
            UnLockMovement();
            FlagInitialize();
            ChangeLayer(baseLayerIndex);
            EndAllAnimations();
            Animator.Play("Idle");
        }

        public void EndFlyAnimation()
        {
            Animator.Play("Fly_Drop", CurrentLayerIndex, 0);
        }

        public void EndFlySettingFlag()
        {
            ChangeLayer(baseLayerIndex);
            EndAllAnimations();
            FlagInitialize();
        }

        public void EndHasegiAnimation()
        {
            UnLockMovement();
            FlagInitialize();
            ChangeLayer(baseLayerIndex);
            EndAllAnimations();
            Animator.Play("Idle");
        }

        public void ShootingHasegi()
        {
            ShootHasegi = true;
        }

        protected override void EndAllAnimations()
        {
            base.EndAllAnimations();
            Animator.SetBool("HasegiExit",true);
            Animator.SetBool("FlyExit",true);
            Animator.SetBool("UpperWingExit",true);
            Animator.SetBool("ScratchExit",true);
        }
        
        protected override void FlagInitialize()
        {
            PunchFlagInitialize();
            motionFlag = false;
        }
    }
}