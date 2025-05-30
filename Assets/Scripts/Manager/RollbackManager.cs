using System.Collections.Generic;
using Movement;
using UnityEngine;

namespace Manager
{
    public class RollbackManager : MonoBehaviour
    {
        public static RollbackManager Manager { get; private set; }

        private InputDictionary inputDictionary;
        private Dictionary<int, PlayerState> stateHistory;

        public int CurrentFrame { get; private set; }

        private bool isRollingBack = false;

        void Awake()
        {
            if (Manager == null)
                Manager = this;

            inputDictionary = new InputDictionary();
            stateHistory = new Dictionary<int, PlayerState>();
        }

        public void ProcessingMessage(string message)
        {
            string[] splitMessage = message.Split(Constant.SteamNetworkingType.DELIMITER);
            int type = int.Parse(splitMessage[0]);
            int frame = int.Parse(splitMessage[1]);

            FrameInput input = inputDictionary.GetRemote(frame);

            switch (type)
            {
                case Constant.SteamNetworkingType.KeyInput.MOVEMENT:
                    input.JumpInput = bool.Parse(splitMessage[2]);
                    input.MoveInput = int.Parse(splitMessage[3]);
                    input.RemotePosition = new Vector2(-1 * float.Parse(splitMessage[4]), float.Parse(splitMessage[5]));
                    input.SkillInput = splitMessage[6];
                    break;

                case Constant.SteamNetworkingType.KeyInput.SKILL:
                    input.SkillInput = splitMessage[2];
                    break;
            }

            // 롤백 트리거
            if (frame < CurrentFrame)
            {
                Debug.Log($"Rollback from frame {frame}");
                isRollingBack = true;
                RestoreState(frame - 1);

                for (int f = frame; f < CurrentFrame; f++)
                {
                    Simulate(f);
                }

                isRollingBack = false;
            }
        }

        public void AdvanceFrame(int localInput, bool localJump, string localSkill)
        {
            FrameInput input = inputDictionary.GetLocal(CurrentFrame);
            input.MoveInput = localInput;
            input.JumpInput = localJump;
            input.SkillInput = localSkill;

            Simulate(CurrentFrame);

            CurrentFrame++;
        }

        private void RestoreState(int frame)
        {
            if (stateHistory.TryGetValue(frame, out PlayerState state))
            {
                var player = VarManager.Manager.PlayerGameObject.transform.GetChild(0);
                var opponent = VarManager.Manager.OpponentGameObject.transform.GetChild(0);

                player.position = state.Player1Position;
                opponent.position = state.Player2Position;

                VarManager.Manager.Player.Position = state.Player1Position;
                VarManager.Manager.Opponent.Position = state.Player2Position;
            }
        }

        private void Simulate(int frame)
        {
            FrameInput local = inputDictionary.GetLocal(frame);
            FrameInput remote = inputDictionary.GetRemote(frame);

            var playerObj = VarManager.Manager.PlayerGameObject.transform.GetChild(0).gameObject;
            var opponentObj = VarManager.Manager.OpponentGameObject.transform.GetChild(0).gameObject;

            // 이동
            CharacterMovementController.MoveCharacter(playerObj, local.MoveInput);
            CharacterMovementController.MoveCharacter(opponentObj, remote.MoveInput);

            // 애니메이션 처리
            if (remote.MoveInput == 0)
                VarManager.Manager.Opponent.Animator.EndWalkAnimation();
            else
                VarManager.Manager.Opponent.Animator.StartWalkAnimation();

            // 점프
            if (local.JumpInput)
                CharacterMovementController.JumpCharacter(playerObj);

            if (remote.JumpInput)
                CharacterMovementController.JumpCharacter(opponentObj);

            // 스킬
            if (!string.IsNullOrEmpty(local.SkillInput))
                VarManager.Manager.PlayerSkills[TranslateKorToEng(local.SkillInput)].Run();

            if (!string.IsNullOrEmpty(remote.SkillInput))
                VarManager.Manager.OpponentSkills[TranslateKorToEng(remote.SkillInput)].Run();

            // 위치 보정: Rollback 중일 때만 상대 위치를 강제 덮어쓰기
            if (isRollingBack)
            {
                opponentObj.transform.position = remote.RemotePosition;
                VarManager.Manager.Opponent.Position = remote.RemotePosition;
            }

            // 상태 저장
            stateHistory[frame] = new PlayerState(
                playerObj.transform.position,
                opponentObj.transform.position
            );
        }

        private string TranslateKorToEng(string kor)
        {
            return kor switch
            {
                "할퀴기" => "Scratch",
                "어퍼윙" => "UpperWing",
                _ => kor
            };
        }

        public class FrameInput
        {
            public int MoveInput = 0;
            public bool JumpInput = false;
            public string SkillInput = string.Empty;
            public Vector2 RemotePosition = Vector2.zero;

            public FrameInput Clone()
            {
                return new FrameInput
                {
                    MoveInput = this.MoveInput,
                    JumpInput = this.JumpInput,
                    SkillInput = this.SkillInput,
                    RemotePosition = this.RemotePosition
                };
            }
        }

        public class InputDictionary
        {
            public Dictionary<int, FrameInput> LocalInput = new Dictionary<int, FrameInput>();
            public Dictionary<int, FrameInput> RemoteInput = new Dictionary<int, FrameInput>();

            public FrameInput GetLocal(int frame) => GetFrameInput(LocalInput, frame);
            public FrameInput GetRemote(int frame) => GetFrameInput(RemoteInput, frame);

            private FrameInput GetFrameInput(Dictionary<int, FrameInput> dict, int frame)
            {
                if (!dict.TryGetValue(frame, out FrameInput input))
                {
                    input = new FrameInput();
                    dict[frame] = input;
                }

                return input;
            }
        }

        private class PlayerState
        {
            public Vector2 Player1Position, Player2Position;

            public PlayerState(Vector2 player1, Vector2 player2)
            {
                Player1Position = player1;
                Player2Position = player2;
            }

            public PlayerState Clone()
            {
                return new PlayerState(Player1Position, Player2Position);
            }
        }
    }
}