using UnityEngine;
using RollbackNetcode.State;
using RollbackNetcode.SetState;

namespace RollbackNetcode.StateSimulator
{
    public class ActiveStateSimulator : StateSimulatorBase
    {
        public override void Start()
        {
            base.Start();
            SetStateBase = new SetActive();
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
            if (!LocalStates.ContainsKey(frame))
            {
                LocalStates.Add(frame, new ActiveState());
            }
            if (!RemoteStates.ContainsKey(frame))
            {
                RemoteStates.Add(frame, new ActiveState());
            }
            
            LocalStates[frame + 1] = new ActiveState();
            RemoteStates[frame + 1] = new ActiveState();
        }
    }
}