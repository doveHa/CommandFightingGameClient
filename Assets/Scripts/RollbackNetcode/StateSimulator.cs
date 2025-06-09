using System;
using System.Collections.Generic;
using Movement;
using UnityEngine;

namespace RollbackNetcode
{
    public abstract class StateSimulator : MonoBehaviour
    {
        protected SetState SetState { get; set; }
        public static int CurrentFrame { get; set; } = 0;
        protected const int FRAME = 0, VALUE = 1;
        protected Dictionary<int, State> LocalStates, RemoteStates;

        public virtual void Start()
        {
            LocalStates = new Dictionary<int, State>();
            RemoteStates = new Dictionary<int, State>();
        }

        void FixedUpdate()
        {
            SetState.ApplyState();
            PredictionFrame(CurrentFrame);
            LocalStates[CurrentFrame].Simulate(true);
            RemoteStates[CurrentFrame].Simulate(false);
        }

        public void AddState(int frame, State state)
        {
            LocalStates[frame] = state;
        }

        public abstract void ProcessingMessage(string msg);
        protected abstract void PredictionFrame(int frame);

        protected void RestoreState(int frame)
        {
            for (int i = frame; i <= CurrentFrame; i++)
            {
                PredictionFrame(i);
                RemoteStates[i].Simulate(false);
            }
        }
    }
}