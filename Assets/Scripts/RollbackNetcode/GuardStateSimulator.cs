using System.Collections.Generic;
using Movement;
using UnityEngine.SocialPlatforms;

namespace RollbackNetcode
{
    public class GuardStateSimulator : StateSimulator
    {
        public override void Start()
        {
            base.Start();
            SetState = new SetGuard();
            LocalStates.Add(0, new GuardState());
            RemoteStates.Add(0, new GuardState());
        }

        public override void ProcessingMessage(string msg)
        {
            string[] splits = msg.Split(Constant.SteamNetworkingType.DELIMITER);
            int frame = int.Parse(splits[FRAME]);

            RemoteStates[frame] = new GuardState(bool.Parse(splits[VALUE]));

            if (frame <= CurrentFrame)
            {
                RestoreState(frame);
            }
        }

        protected override void PredictionFrame(int frame)
        {
            if (!LocalStates.ContainsKey(frame))
            {
                LocalStates.Add(frame, new GuardState());
            }
            if (!RemoteStates.ContainsKey(frame))
            {
                RemoteStates.Add(frame, new GuardState());
            }

            LocalStates[frame + 1] = LocalStates[frame];
            RemoteStates[frame + 1] = RemoteStates[frame];
        }
    }
}