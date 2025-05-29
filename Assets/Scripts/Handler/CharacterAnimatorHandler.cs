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
        private int BASE_LAYER_INDEX, HIT_LAYER_INDEX, GUARD_LAYER_INDEX;
        protected Animator Animator;
        protected Transform PlayerTransform;

        private Dictionary<string, bool> animationFlag;
        protected bool motionFlag = false;

        protected FrameRangesDictionary dictionary;
        public string State { get; set; }
        public int FrameIndex { get; set; }

        public GameObject Center { get; private set; }

        protected virtual void Awake()
        {
            Center = transform.Find("Center").gameObject;

            Animator = GetComponent<Animator>();
            PlayerTransform = transform;

            animationFlag = new Dictionary<string, bool>();
            animationFlag.Add("Punch", false);

            BASE_LAYER_INDEX = Animator.GetLayerIndex("BaseLayer");
            HIT_LAYER_INDEX = Animator.GetLayerIndex("HitLayer");
            GUARD_LAYER_INDEX = Animator.GetLayerIndex("GuardLayer");
        }

        protected virtual void FixedUpdate()
        {
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
        }

        public void StartGuardAnimation()
        {
            Animator.SetLayerWeight(BASE_LAYER_INDEX, 0);
            Animator.SetLayerWeight(GUARD_LAYER_INDEX, 1);
        }

        public void EndGuardAnimation()
        {
            Animator.SetLayerWeight(GUARD_LAYER_INDEX, 0);
            Animator.SetLayerWeight(BASE_LAYER_INDEX, 1);
        }

        public void StartAirborneAnimation()
        {
            Animator.SetTrigger("Airborne");
            ChangeHitLayer();
        }

        public void ReAirborneHitAnimation()
        {
            ChangeHitLayer();
            Animator.Play("Airborne");
        }

        public void ChangeHitLayer()
        {
            Animator.SetLayerWeight(BASE_LAYER_INDEX, 0);
            Animator.SetLayerWeight(HIT_LAYER_INDEX, 1);
        }

        public void EndHitAnimation()
        {
            Animator.SetLayerWeight(BASE_LAYER_INDEX, 1);
            Animator.SetLayerWeight(HIT_LAYER_INDEX, 0);
            InputActionManager.Manager.UnLockInput();
            GetComponentInParent<Player>().HitEnd();
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