using System.Collections.Generic;
using Movement;
using UnityEngine.SocialPlatforms;

namespace RollbackNetcode
{
    public class JumpStateSimulator : StateSimulator
    {
        public override void Start()
        {
            base.Start();
            SetState = new SetJump();
            LocalStates.Add(0, new JumpState());
            RemoteStates.Add(0, new JumpState());
        }

        public override void ProcessingMessage(string msg)
        {
            string[] splits = msg.Split(Constant.SteamNetworkingType.DELIMITER);
            int frame = int.Parse(splits[FRAME]);

            RemoteStates[frame] = new JumpState(bool.Parse(splits[VALUE]));

            if (frame <= CurrentFrame)
            {
                RestoreState(frame);
            }
        }

        protected override void PredictionFrame(int frame)
        {
            LocalStates[frame + 1] = new JumpState();
            RemoteStates[frame + 1] = new JumpState();
        }
    }
}