using UnityEngine;
using System.Collections.Generic;
using System.Data;
using DataTable.FrameRanges;
using DataTable.DataSet;
using Manager;
using DataSet = DataTable.DataSet.DataSet;

namespace Handler
{
    public abstract class CharacterAnimatorHandler : MonoBehaviour
    {
        protected int baseLayerIndex, punchLayerIndex;

        protected Animator Animator;
        protected Transform PlayerTransform;

        private bool punchFlag;
        private bool additionalPunch;
        private Dictionary<string, bool> animationFlag;
        protected bool motionFlag = false;

        protected int CurrentLayerIndex;
        protected FrameRangesDictionary dictionary;
        public string State { get; set; }
        public int FrameIndex { get; set; }

        public GameObject Center { get; private set; }

        protected virtual void Awake()
        {
            CurrentLayerIndex = baseLayerIndex;
            Center = transform.Find("Center").gameObject;

            Animator = GetComponent<Animator>();
            PlayerTransform = transform;

            animationFlag = new Dictionary<string, bool>();
            animationFlag.Add("Punch", false);
            animationFlag.Add("Punch2", false);

            punchFlag = false;

            baseLayerIndex = Animator.GetLayerIndex("BaseLayer");
            punchLayerIndex = Animator.GetLayerIndex("Punch");
        }

        protected virtual void FixedUpdate()
        {
            CalFrameNumber();
        }

        public void StartPunchAnimation()
        {
            if (punchFlag)
            {
                additionalPunch = true;
            }
            else
            {
                ChangeLayer(punchLayerIndex);
                Animator.SetBool("PunchExit", false);
                Animator.Play("Atk_Punch", CurrentLayerIndex, 0);
                punchFlag = true;
            }
        }

        public void AdditionalPunchAnimation()
        {
            if (additionalPunch)
            {
                Animator.SetTrigger("AdditionalPunch");
                additionalPunch = false;
            }
        }

        public void EndPunchAnimation()
        {
            Debug.Log("EndPunchAnimation Method");
            Debug.Log(additionalPunch);
            if (!additionalPunch)
            {
                Debug.Log("EndPunch");
                PunchFlagInitialize();
                ChangeLayer(baseLayerIndex);
                Animator.SetBool("PunchExit", true);
            }
        }

        protected void PunchFlagInitialize()
        {
            punchFlag = false;
            additionalPunch = false;
        }

        public void StartHitAnimation()
        {
            Animator.SetBool("Hit", true);
        }

        public void FlagPunchFalse()
        {
            animationFlag["Punch"] = false;
            animationFlag["Punch2"] = false;
            motionFlag = false;
        }

        public void StartGuardAnimation()
        {
            Animator.SetLayerWeight(baseLayerIndex, 0);
            Animator.SetLayerWeight(baseLayerIndex, 1);
        }

        public void EndGuardAnimation()
        {
            Animator.SetLayerWeight(baseLayerIndex, 0);
            Animator.SetLayerWeight(baseLayerIndex, 1);
        }

        public void StartAirborneAnimation()
        {
            Animator.SetBool("Airborne", true);
        }

        public void ReAirborneHitAnimation()
        {
            Animator.Play("Airborne");
        }

        public void EndAirborneHitAnimation()
        {
        }

        public void EndHitAnimation()
        {
            Animator.SetBool("Hit", false);
        }

        public void StartWalkAnimation()
        {
            Animator.SetBool("IsMove", true);
        }

        public void EndWalkAnimation()
        {
            Animator.SetBool("IsMove", false);
        }

        protected void ChangeLayer(int targetLayerIndex)
        {
            Animator.SetLayerWeight(CurrentLayerIndex, 0.01f);
            Animator.SetLayerWeight(targetLayerIndex, 1);
            CurrentLayerIndex = targetLayerIndex;
        }


        private void CalFrameNumber()
        {
            AnimatorStateInfo stateInfo = Animator.GetCurrentAnimatorStateInfo(CurrentLayerIndex);
            if (!stateInfo.IsName("Empty"))
            {
                float normalizedTime = stateInfo.normalizedTime % 1f;

                AnimatorClipInfo[] clipInfo = Animator.GetCurrentAnimatorClipInfo(CurrentLayerIndex);
                AnimationClip clip = clipInfo[0].clip;
                Debug.Log(CurrentLayerIndex);
                int totalFrames = Mathf.RoundToInt(clip.length * clip.frameRate);
                int currentFrame = Mathf.FloorToInt(normalizedTime * totalFrames);

                State = clip.name;
                List<FrameRange> frameRanges =
                    dictionary.FrameRanges[State];
                for (int i = 0; i < 4; i++)
                {
                    if (currentFrame >= frameRanges[i].start && currentFrame <= frameRanges[i].end)
                    {
                        FrameIndex = i;
                        break;
                    }
                }

                if (gameObject.CompareTag("Player"))
                {
                    HitBoxManager.Manager.SetPlayerState(State, FrameIndex);
                }

                if (gameObject.CompareTag("Opponent"))
                {
                    HitBoxManager.Manager.SetOpponentState(State, FrameIndex);
                }
            }
        }

        private void OnDrawGizmos()
        {
            foreach (CharacterAllStatement statement in VarManager.Manager.Opponent.DataSet.RawData)
            {
                if (statement.Statement.Equals(State))
                {
                    foreach (var box in statement.FrameData[FrameIndex].HurtBoxes)
                    {
                        Vector2 playerCenter = Vector2.zero;
                        Vector2 center = Vector2.zero;
                        Vector2 size = Vector2.zero;
                        if (box.PartName.Equals("HitBox"))
                        {
                            Gizmos.color = Color.red;
                            playerCenter = PlayerTransform.position;
                            center = playerCenter + DataSet.FloatArrayToVector2(box.OffSet);
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