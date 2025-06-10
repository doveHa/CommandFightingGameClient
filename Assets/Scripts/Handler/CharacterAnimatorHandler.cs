using UnityEngine;
using System.Collections.Generic;
using System.Data;
using DataTable.FrameRanges;
using DataTable.DataSet;
using Manager;
#if UNITY_EDITOR
using UnityEditor.Searcher;
#endif
using DataSet = DataTable.DataSet.DataSet;

namespace Handler
{
    public abstract class CharacterAnimatorHandler : MonoBehaviour
    {
        protected int baseLayerIndex, punchLayerIndex, endLayerIndex;

        protected Animator Animator;
        protected Transform PlayerTransform;

        private bool punchFlag;
        private bool additionalPunch;
        private Dictionary<string, bool> animationFlag;
        protected bool motionFlag = false;

        protected int CurrentLayerIndex;
        protected FrameRangesDictionary dictionary;

        protected abstract void FlagInitialize();

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
            animationFlag.Add("Punch2", false);

            punchFlag = false;

            baseLayerIndex = Animator.GetLayerIndex("BaseLayer");
            punchLayerIndex = Animator.GetLayerIndex("Punch");
            endLayerIndex = Animator.GetLayerIndex("EndLayer");

            CurrentLayerIndex = baseLayerIndex;
        }

        protected virtual void FixedUpdate()
        {
            CalFrameNumber();
        }

        public void StartPunchAnimation(float startTime)
        {
            if (motionFlag)
            {
            }
            else
            {
                if (punchFlag && !additionalPunch)
                {
                    additionalPunch = true;
                }

                if (!punchFlag)
                {
                    LockMovement();
                    ChangeLayer(punchLayerIndex);
                    Animator.SetBool("PunchExit", false);
                    Animator.Play("공격", CurrentLayerIndex, startTime);
                    punchFlag = true;
                }
            }
        }

        public void StartJumpPunchAnimation(float startTime)
        {
            if (!motionFlag)
            {
                Animator.Play("점프공격", CurrentLayerIndex, startTime);
                motionFlag = true;
            }
        }

        public void AdditionalPunchAnimation()
        {
            if (additionalPunch)
            {
                Animator.SetTrigger("AdditionalPunch");
                LockMovement();
            }
            else
            {
                punchFlag = false;
            }
        }

        public void EndPunchAnimation()
        {
            if (!additionalPunch && !motionFlag)
            {
                PunchFlagInitialize();
                ChangeLayer(baseLayerIndex);
                EndAllAnimations();
                UnLockMovement();
            }
        }


        public void EndJumpPunchAnimation()
        {
            EndAllAnimations();
            motionFlag = false;
        }

        public void EndKickAnimation()
        {
            if (!motionFlag)
            {
                PunchFlagInitialize();
                ChangeLayer(baseLayerIndex);
                EndAllAnimations();
                UnLockMovement();
            }
        }

        protected void PunchFlagInitialize()
        {
            punchFlag = false;
            additionalPunch = false;
        }

        public void StartHitAnimation()
        {
            LockMovement();
            // 입력 잠금
            ChangeLayer(baseLayerIndex);
            Animator.SetBool("Hit", true);
            motionFlag = true;
            FlagInitialize();
        }

        public void StartGuardAnimation()
        {
            LockMovement();
            Animator.SetBool("IsGuard", true);
        }

        public void StartWinAnimation()
        {
            LockMovement();
            ChangeLayer(endLayerIndex);
            Animator.Play("Win", CurrentLayerIndex, 0);
        }

        public void StartLoseAnimation()
        {
            LockMovement();
            ChangeLayer(endLayerIndex);
            Animator.Play("Lose", CurrentLayerIndex, 0);
        }

        public void EndGuardAnimation()
        {
            Animator.SetBool("IsGuard", false);
            UnLockMovement();
        }

        public void EndHitAnimation()
        {
            UnLockMovement();
            //입력 잠금 해제
            motionFlag = false;
            Animator.SetBool("Hit", false);
            EndAllAnimations();
        }

        public void StartWalkAnimation()
        {
            Animator.SetBool("IsMove", true);
        }

        public void EndWalkAnimation()
        {
            Animator.SetBool("IsMove", false);
        }

        public bool StartJumpAnimation()
        {
            if (CurrentLayerIndex == baseLayerIndex)
            {
                Animator.SetTrigger("IsJump");
                return true;
            }

            return false;
        }

        public void EndJumpAnimation()
        {
            Animator.Play("Jumping_Down", baseLayerIndex, 0);
            motionFlag = false;
            EndAllAnimations();
        }

        protected virtual void EndAllAnimations()
        {
            Animator.SetBool("PunchExit", true);
        }

        protected void ChangeLayer(int targetLayerIndex)
        {
            Animator.SetLayerWeight(CurrentLayerIndex, 0);
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
                int totalFrames = Mathf.RoundToInt(clip.length * clip.frameRate);
                int currentFrame = Mathf.FloorToInt(normalizedTime * totalFrames);

                State = clip.name;
                List<FrameRange> frameRanges =
                    dictionary.FrameRanges[State];
                for (int i = 0; i < frameRanges.Count; i++)
                {
                    if (currentFrame >= frameRanges[i].start && currentFrame <= frameRanges[i].end)
                    {
                        FrameIndex = i;
                        break;
                    }
                }

                if (gameObject.transform.parent.CompareTag("Player"))
                {
                    VarManager.Manager.PlayerHitBoxHandler.SetCurrentState(State, FrameIndex);
                    VarManager.Manager.OpponentHitBoxHandler.SetOpponentState(State, FrameIndex);
                    //HitBoxManager.Manager.SetPlayerState(State, FrameIndex);
                }

                if (gameObject.transform.parent.CompareTag("Opponent"))
                {
                    VarManager.Manager.OpponentHitBoxHandler.SetCurrentState(State, FrameIndex);
                    VarManager.Manager.PlayerHitBoxHandler.SetOpponentState(State, FrameIndex);
                    //HitBoxManager.Manager.SetOpponentState(State, FrameIndex);
                }
            }
        }
/*
        private void OnDrawGizmos()
        {
            foreach (CharacterAllStatement statement in GetComponentInParent<Player>().DataSet.RawData)
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
*/
        protected void LockMovement()
        {
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
        }

        protected void UnLockMovement()
        {
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
}