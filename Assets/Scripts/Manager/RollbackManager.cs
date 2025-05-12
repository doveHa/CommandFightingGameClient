using System;
using System.Collections.Generic;
using UnityEngine;
using Movement;

namespace RollbackNetCode
{
    public class RollbackManager : MonoBehaviour
    {
        public static RollbackManager Manager { get; private set; }

        public InputDictionary inputDictionary;
        private Dictionary<int, PlayerState> stateHistory;

        [SerializeField] private Player player;
        [SerializeField] private Player opponent;

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

        public void ProcessingMessage(string message)
        {
            string[] splitMessage = message.Split(Constant.SteamNetworkingType.DELIMITER);
            int type = int.Parse(splitMessage[0]);
            int frame = int.Parse(splitMessage[1]);

            bool changed = false;

            FrameInput input = inputDictionary.GetRemote(frame);
            switch (type)
            {
                //splitMessage[2] = -1, 0, 1 
                case Constant.SteamNetworkingType.KeyInput.MOVEMENT:
                    input.MoveInput = int.Parse(splitMessage[2]);
                    changed = true;
                    break;
                //splitMessage[2] = String.Empty
                case Constant.SteamNetworkingType.KeyInput.JUMP:
                    Debug.Log(frame + "Receive JUMP");
                    input.JumpInput = true;
                    changed = true;
                    break;
                //splitMessage[2] = SKillName
                case Constant.SteamNetworkingType.KeyInput.SKILL:
                    input.SkillInput = splitMessage[2];
                    changed = true;
                    break;
            }
/*
            if (changed && frame < CurrentFrame)
            {
                ForceRollbackFrom(frame);
            }
 */       }
        private void ForceRollbackFrom(int frame)
        {
            RollbackTo(frame);
            for (int f = frame; f < CurrentFrame; f++)
            {
                Simulate(f);
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
                RollbackTo(rollbackStart);
            }
            else
            {
                Simulate(CurrentFrame);
            }

            CurrentFrame++;
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


        private void RollbackTo(int rollbackStart)
        {
            Debug.Log($"[RollbackManager] Rolling back to frame {rollbackStart}");
            if (!stateHistory.ContainsKey(rollbackStart)) return;
            var state = stateHistory[rollbackStart].Clone();
            player.Position = state.Player1Position;
            opponent.Position = state.Player2Position;

            for (int frame = rollbackStart; frame < CurrentFrame; frame++)
            {
                Simulate(frame);
            }
        }

        private void Simulate(int frame)
        {
            FrameInput local = inputDictionary.GetLocal(frame);
            FrameInput remote = inputDictionary.GetRemote(frame);

            CharacterMovementController.MoveCharacter(player.gameObject,local.MoveInput);
            /*CharacterMovementController.MoveCharacter(player.transform.GetChild(0).GetChild(0).gameObject,
                local.MoveInput);
            */
            CharacterMovementController.MoveCharacter(opponent.transform.GetChild(0).GetChild(0).gameObject,
                remote.MoveInput);

            if (local.JumpInput)
            {
                Debug.Log(CurrentFrame + "JUMP!" + remote.JumpInput);
                CharacterMovementController.JumpCharacter(player.gameObject);
            }

            if (remote.JumpInput)
            {
                Debug.Log("REMOTE JUMP!");

                CharacterMovementController.JumpCharacter(opponent.gameObject);
            }

            if (!string.IsNullOrEmpty(local.SkillInput))
            {
                player.UseSkill(local.SkillInput);
            }

            if (!string.IsNullOrEmpty(remote.SkillInput))
            {
                opponent.UseSkill(remote.SkillInput);
            }

            stateHistory[frame] = new PlayerState(player.Position, opponent.Position);
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