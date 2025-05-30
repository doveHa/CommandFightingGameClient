using System.Collections.Generic;
using Manager;
using UnityEngine;
using Movement;
using UnityEngine.Rendering;

namespace Manager
{
    public class RollbackManager : MonoBehaviour
    {
        public static RollbackManager Manager { get; private set; }

        private InputDictionary inputDictionary;
        private Dictionary<int, PlayerState> stateHistory;


        public class FrameInput
        {
            public int MoveInput = 0;
            public bool JumpInput = false;
            public string SkillInput = string.Empty;

            public FrameInput Clone()
            {
                return new FrameInput
                {
                    MoveInput = this.MoveInput,
                    JumpInput = this.JumpInput,
                    SkillInput = this.SkillInput
                };
            }
        }

        public int CurrentFrame { get; private set; }

        void Awake()
        {
            if (Manager == null)
            {
                Manager = this;
            }

            inputDictionary = new InputDictionary();
            stateHistory = new Dictionary<int, PlayerState>();
        }

        void Start()
        {
        }
/*
        public void ProcessingMessage(string message)
        {
            string[] splitMessage = message.Split(Constant.SteamNetworkingType.DELIMITER);
            int type = int.Parse(splitMessage[0]);
            int frame = int.Parse(splitMessage[1]);

            bool changed = false;

            FrameInput input = inputDictionary.GetRemote(frame);
            switch (type)
            {
                //splitMessage[2] = isJump > -1, 0, 1 
                case Constant.SteamNetworkingType.KeyInput.MOVEMENT:
                    input.JumpInput = bool.Parse(splitMessage[2]);
                    input.MoveInput = int.Parse(splitMessage[3]);
                    input.SkillInput = splitMessage[4];
                    changed = true;
                    break;
                //splitMessage[2] = SKillName
                case Constant.SteamNetworkingType.KeyInput.SKILL:
                    input.SkillInput = splitMessage[2];
                    changed = true;
                    break;
            }
        }
        */
        public void ProcessingMessage(string message)
        {
            string[] splitMessage = message.Split(Constant.SteamNetworkingType.DELIMITER);
            int type = int.Parse(splitMessage[0]);
            int frame = int.Parse(splitMessage[1]);

            bool changed = false;

            FrameInput input = inputDictionary.GetRemote(frame);
            switch (type)
            {
                case Constant.SteamNetworkingType.KeyInput.MOVEMENT:
                    input.JumpInput = bool.Parse(splitMessage[2]);
                    input.MoveInput = int.Parse(splitMessage[3]);
                    input.SkillInput = splitMessage[4];
                    changed = true;
                    break;

                case Constant.SteamNetworkingType.KeyInput.SKILL:
                    input.SkillInput = splitMessage[2];
                    changed = true;
                    break;
            }

            if (changed && frame < CurrentFrame)
            {
                Debug.Log($"[Remote Correction Detected] Rolling back from frame {frame}");

                RestoreState(frame - 1);

                for (int f = frame; f < CurrentFrame; f++)
                {
                    Simulate(f);
                }
            }
        }


        public void AdvanceFrame(int localInput, bool localJump, string localSkill)
        {
            FrameInput input = inputDictionary.GetLocal(CurrentFrame);
            input.MoveInput = localInput;
            input.JumpInput = localJump;
            input.SkillInput = localSkill;

            int rollbackStart = FindRollbackStart();

            if (rollbackStart < CurrentFrame)
            {
                Debug.Log("RollBack");
                RestoreState(rollbackStart - 1);

                for (int f = rollbackStart; f <= CurrentFrame; f++)
                {
                    Simulate(f);
                }
            }
            else
            {
                Simulate(CurrentFrame);
            }

            CurrentFrame++;
        }

        private void RestoreState(int frame)
        {
            if (stateHistory.TryGetValue(frame, out PlayerState state))
            {
                VarManager.Manager.Player.Position = state.Player1Position;
                VarManager.Manager.Opponent.Position = state.Player2Position;

                // Transform도 이동
                VarManager.Manager.PlayerGameObject.transform.GetChild(0).position = state.Player1Position;
                VarManager.Manager.OpponentGameObject.transform.GetChild(0).position = state.Player2Position;

                Debug.Log(
                    $"[RestoreState] Frame {frame} - Player: {state.Player1Position}, Opponent: {state.Player2Position}");
            }
            else
            {
                Debug.LogWarning($"[RestoreState] No saved state at frame {frame}");
            }
        }

        private int FindRollbackStart()
        {
            for (int f = CurrentFrame - 1; f >= CurrentFrame - 10; f--)
            {
                if (inputDictionary.RemoteInput.ContainsKey(f))
                {
                    return f + 1;
                }
            }

            return CurrentFrame;
        }

        private void Simulate(int frame)
        {
            FrameInput local = inputDictionary.GetLocal(frame);
            FrameInput remote = inputDictionary.GetRemote(frame);

            CharacterMovementController.MoveCharacter(
                VarManager.Manager.PlayerGameObject.transform.GetChild(0).gameObject, local.MoveInput);

            CharacterMovementController.MoveCharacter(
                VarManager.Manager.OpponentGameObject.transform.GetChild(0).gameObject, remote.MoveInput);
            if (remote.MoveInput == 0)
            {
                VarManager.Manager.Opponent.Animator.EndWalkAnimation();
            }
            else
            {
                VarManager.Manager.Opponent.Animator.StartWalkAnimation();
            }

            if (local.JumpInput)
            {
                Debug.Log(CurrentFrame + "JUMP!" + remote.JumpInput);
                CharacterMovementController.JumpCharacter(VarManager.Manager.PlayerGameObject.transform.GetChild(0)
                    .gameObject);
            }

            if (remote.JumpInput)
            {
                Debug.Log("REMOTE JUMP!");
                CharacterMovementController.JumpCharacter(VarManager.Manager.OpponentGameObject.transform.GetChild(0)
                    .gameObject);
            }

            if (!string.IsNullOrEmpty(local.SkillInput))
            {
                VarManager.Manager.PlayerSkills[TranslateKorToEng(local.SkillInput)].Run();
            }

            if (!string.IsNullOrEmpty(remote.SkillInput))
            {
                VarManager.Manager.OpponentSkills[TranslateKorToEng(remote.SkillInput)].Run();
            }

            stateHistory[frame] =
                new PlayerState(VarManager.Manager.Player.Position, VarManager.Manager.Opponent.Position);
        }

        private string TranslateKorToEng(string kor)
        {
            switch (kor)
            {
                case "할퀴기":
                    return "Scratch";
                case "어퍼윙":
                    return "UpperWing";
                default:
                    return kor;
            }
        }

        public class InputDictionary
        {
            public Dictionary<int, FrameInput> LocalInput = new Dictionary<int, FrameInput>();
            public Dictionary<int, FrameInput> RemoteInput = new Dictionary<int, FrameInput>();

            public FrameInput GetLocal(int frame)
            {
                return GetFrameInput(LocalInput, frame);
            }

            public FrameInput GetRemote(int frame)
            {
                return GetFrameInput(RemoteInput, frame);
            }

            private FrameInput GetFrameInput(Dictionary<int, FrameInput> dictionary, int frame)
            {
                if (!dictionary.TryGetValue(frame, out FrameInput input))
                {
                    input = new FrameInput();
                    dictionary[frame] = input;
                }

                return input;
            }
        }

        private class PlayerState
        {
            public Vector2 Player1Position, Player2Position;

            public PlayerState(Vector2 player1Position, Vector2 player2Position)
            {
                Player1Position = player1Position;
                Player2Position = player2Position;
            }

            public PlayerState Clone()
            {
                return new PlayerState(Player1Position, Player2Position);
            }
        }
    }
}