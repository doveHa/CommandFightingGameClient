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
        protected bool motionFlag = false;

        protected FrameRangesDictionary dictionary;
        protected string currentClip;
        protected int frameIndex;
        
        public GameObject Center { get; private set; }

        protected virtual void Start()
        {
            Center = transform.Find("Center").gameObject;
            
            Animator = GetComponent<Animator>();
            PlayerTransform = transform;

            animationFlag = new Dictionary<string, bool>();
            animationFlag.Add("Punch", false);
        }

        protected virtual void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartPunchAnimation();
            }
            
            CalFrameNumber();
        }

        public void StartPunchAnimation()
        {
            if (!animationFlag["Punch"] && !motionFlag)
            {
                motionFlag = true;
                animationFlag["Punch"] = true;
                Animator.SetTrigger("Punch");
            }
        }

        public void FlagPunchFalse()
        {
            animationFlag["Punch"] = false;
            motionFlag = false;
            Debug.Log(motionFlag);
        }

        public void StartWalkAnimation()
        {
            Animator.SetBool("IsMove", true);
        }

        public void EndWalkAnimation()
        {
            Animator.SetBool("IsMove", false);
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
                dictionary.FrameRanges[currentClip];
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