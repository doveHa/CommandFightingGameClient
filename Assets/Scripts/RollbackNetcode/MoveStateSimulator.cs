using System.Collections.Generic;
using Movement;
using UnityEngine.tvOS;

namespace RollbackNetcode
{
    public class MoveStateSimulator : StateSimulator
    {
        public override void Start()
        {
            base.Start();
            SetState = new SetMove();
            LocalStates.Add(-1, new MoveState());
            LocalStates.Add(0, new MoveState());
            LocalStates.Add(1, new MoveState());
            RemoteStates.Add(-1, new MoveState());
            RemoteStates.Add(0, new MoveState());
            RemoteStates.Add(1, new MoveState());
        }

        public override void ProcessingMessage(string msg)
        {
            string[] splits = msg.Split(Constant.SteamNetworkingType.DELIMITER);
            int frame = int.Parse(splits[FRAME]);

            RemoteStates[frame] = new MoveState(int.Parse(splits[VALUE]));

            if (frame <= CurrentFrame)
            {
                RestoreState(frame);
            }
        }

        protected override void PredictionFrame(int frame)
        {
            LocalStates[frame + 1] = LocalStates[frame].Clone();
            RemoteStates[frame + 1] = RemoteStates[frame].Clone();
        }
    }
}