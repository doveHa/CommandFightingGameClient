using Movement;
using UnityEngine;

namespace RollbackNetcode
{
    public class ActiveStateSimulator : StateSimulator
    {
        public override void Start()
        {
            base.Start();
            SetState = new SetActive();
            LocalStates.Add(0, new ActiveState());
            RemoteStates.Add(0, new ActiveState());
        }
        

        public override void ProcessingMessage(string msg)
        {
            string[] splits = msg.Split(Constant.SteamNetworkingType.DELIMITER);
            int frame = int.Parse(splits[FRAME]);

            Debug.Log(splits[VALUE]);
            RemoteStates[frame] = new ActiveState(int.Parse(splits[VALUE]));

            if (frame <= CurrentFrame)
            {
                RestoreState(frame);
            }
        }

        protected override void PredictionFrame(int frame)
        {
            LocalStates[frame + 1] = new ActiveState();
            RemoteStates[frame + 1] = new ActiveState();
        }
    }
}