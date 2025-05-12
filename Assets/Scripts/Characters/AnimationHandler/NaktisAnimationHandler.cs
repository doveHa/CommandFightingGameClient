using System.Collections.Generic;
using Data.FrameRanges;
using DataTable;
using Handler;
using UnityEngine;

namespace Characters.AnimationHandler
{
    public class NaktisAnimationHandler : CharacterAnimatorHandler
    {
        private Fly fly;
        private NaktisFrameRangesDictionary naktisFrameRangesDictionary;

        private string currentClip;
        private int frameIndex;

        private Dictionary<string, bool> animationFlag;

        protected override void Start()
        {
            base.Start();
            fly = GetComponent<Fly>();

            naktisFrameRangesDictionary = new NaktisFrameRangesDictionary();
            animationFlag = new Dictionary<string, bool>();
            animationFlag.Add("Hasegi", false);
            animationFlag.Add("Scratch", false);
            animationFlag.Add("UpperWing", false);
        }

        protected override void Update()
        {
            base.Update();
            CalFrameNumber();

            if (Input.GetKeyDown(KeyCode.Z))
            {
                StartHasegiAnimation();
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                StartScratchAnimation();
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                StartUpperWingAnimation();
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                fly.Run();
            }
        }

        public void StartHasegiAnimation()
        {
            if (!animationFlag["Hasegi"])
            {
                animationFlag["Hasegi"] = true;
                Animator.SetTrigger("Hasegi");
            }
        }

        public void FlagHasegiFalse()
        {
            animationFlag["Hasegi"] = false;
        }

        public void StartScratchAnimation()
        {
            if (!animationFlag["Scratch"])
            {
                animationFlag["Scratch"] = true;
                Animator.SetTrigger("Scratch");
            }
        }

        public void FlagScratchFalse()
        {
            animationFlag["Scratch"] = false;
        }

        public void StartUpperWingAnimation()
        {
            if (!animationFlag["UpperWing"])
            {
                animationFlag["UpperWing"] = true;
                Animator.SetTrigger("UpperWing");
            }
        }

        public void FlagUpperWingFalse()
        {
            animationFlag["UpperWing"] = false;
        }

        private void CalFrameNumber()
        {
            AnimatorStateInfo stateInfo = Animator.GetCurrentAnimatorStateInfo(0);
            float normalizedTime = stateInfo.normalizedTime % 1f;

            AnimatorClipInfo[] clipInfo = Animator.GetCurrentAnimatorClipInfo(0);
            AnimationClip clip = clipInfo[0].clip;

            int totalFrames = Mathf.RoundToInt(clip.length * clip.frameRate);
            int currentFrame = Mathf.FloorToInt(normalizedTime * totalFrames);

            currentClip = clip.name;
            List<FrameRange> frameRanges =
                naktisFrameRangesDictionary.FrameRanges[currentClip];
            for (int i = 0; i < 4; i++)
            {
                if (currentFrame >= frameRanges[i].start && currentFrame <= frameRanges[i].end)
                {
                    frameIndex = i;
                    break;
                }
            }
        }

        private void OnDrawGizmos()
        {
            foreach (CharacterAllStatement statement in NaktisFrameDataSet.DataSet)
            {
                if (statement.Statement.Equals(currentClip))
                {
                    Debug.Log(currentClip);
                    foreach (var box in statement.FrameData[frameIndex].HurtBoxes)
                    {
                        Vector2 playerCenter = Vector2.zero;
                        Vector2 center = Vector2.zero;
                        Vector2 size = Vector2.zero;
                        if (box.PartName.Equals("HitBox"))
                        {
                            Gizmos.color = Color.red;
                            playerCenter = PlayerTransform.position;
                            center = playerCenter + NaktisFrameDataSet.FloatArrayToVector2(box.OffSet);
                            size = NaktisFrameDataSet.FloatArrayToVector2(box.Size);
                        }
                        else
                        {
                            playerCenter = PlayerTransform.position;
                            center = playerCenter + NaktisFrameDataSet.FloatArrayToVector2(box.OffSet);
                            size = NaktisFrameDataSet.FloatArrayToVector2(box.Size);
                            Gizmos.color = Color.green;
                        }

                        Gizmos.DrawWireCube(center, size);
                    }
                }
            }
        }
    }
}