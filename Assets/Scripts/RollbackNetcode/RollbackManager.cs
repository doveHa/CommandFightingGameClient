using UnityEngine;

namespace RollbackNetcode
{
    public class RollbackManager : MonoBehaviour
    {
        public static RollbackManager Manager { get; private set; }
        public int CurrentFrame { get; private set; }

        public Simulator Active { get; private set; }
        public Simulator Jump { get; private set; }
        public Simulator Move { get; private set; }

        void Awake()
        {
            if (Manager == null)
            {
                Manager = this;
            }

            Active = new Simulator();
            Jump = new Simulator();
            Move = new Simulator();
        }

        void FixedUpdate()
        {
            if (!Active.OpponentStates.ContainsKey(CurrentFrame))
            {
                Active.OpponentStates.Add(CurrentFrame, new ActiveState());
            }

            if (!Jump.OpponentStates.ContainsKey(CurrentFrame))
            {
                Jump.OpponentStates.Add(CurrentFrame, new JumpState());
            }

            if (!Move.OpponentStates.ContainsKey(CurrentFrame))
            {
                Move.OpponentStates.Add(CurrentFrame, new MoveState());
            }

            if (CurrentFrame < 5)
            {
                CurrentFrame++;
            }
            else
            {
                Active.Simulate(CurrentFrame - 5);
                Jump.Simulate(CurrentFrame - 5);
                Move.Simulate(CurrentFrame - 5);
                CurrentFrame++;
            }
        }

        private const int FRAME = 0, MOVE = 1, JUMP = 2, ACTIVE = 3;

        public void ProcessingMessage(string msg)
        {
            string[] split = msg.Split(Constant.SteamNetworkingType.DELIMITER);
            int frame = int.Parse(split[FRAME]);

            if (Move.OpponentStates.ContainsKey(frame))
            {
                Move.OpponentStates[frame] = new MoveState(int.Parse(split[MOVE]));
            }
            else
            {
                Move.OpponentStates.Add(frame, new MoveState(int.Parse(split[MOVE])));
            }

            if (Jump.OpponentStates.ContainsKey(frame))
            {
                Jump.OpponentStates[frame] = new JumpState(bool.Parse(split[JUMP]));
            }
            else
            {
                Jump.OpponentStates.Add(frame, new JumpState(bool.Parse(split[JUMP])));
            }

            if (Active.OpponentStates.ContainsKey(frame))
            {
                Active.OpponentStates[frame] = new ActiveState(int.Parse(split[ACTIVE]));
            }
            else
            {
                Active.OpponentStates.Add(frame, new ActiveState(int.Parse(split[ACTIVE])));
            }
        }
    }
}