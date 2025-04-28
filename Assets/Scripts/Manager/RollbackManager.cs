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


        public void AdvanceFrame(int localInput)
        {
            inputDictionary.LocalInput[CurrentFrame] = localInput;

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
            int localLocate = inputDictionary.GetLocal(frame);
            int remoteLocate = inputDictionary.GetRemote(frame);
            Movement.Movement.MoveCharacter(player.transform.GetChild(0).GetChild(0).gameObject, localLocate);
            Movement.Movement.MoveCharacter(opponent.transform.GetChild(0).GetChild(0).gameObject, remoteLocate);

            stateHistory[frame] = new PlayerState(player.Position, opponent.Position);
        }

        public class InputDictionary
        {
            public Dictionary<int, int> LocalInput = new Dictionary<int, int>();
            public Dictionary<int, int> RemoteInput = new Dictionary<int, int>();

            public int GetLocal(int frame)
            {
                return GetLocate(LocalInput, frame);
            }

            public int GetRemote(int frame)
            {
                return GetLocate(RemoteInput, frame);
            }

            private int GetLocate(Dictionary<int, int> dictionary, int frame)
            {
                if (dictionary.TryGetValue(frame, out int locate))
                {
                    return locate;
                }

                return 0;
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