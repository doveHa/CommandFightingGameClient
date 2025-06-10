using System;
using System.Collections.Generic;
using Movement;
using UnityEngine;

namespace RollbackNetcode
{
    public abstract class StateSimulator : MonoBehaviour
    {
        protected SetState SetState { get; set; }
        public static int CurrentFrame => CalculateCurrentFrame();
        public static long SharedStartTimeMs;
        public static long TimeOffsetFromSharedStart;

        protected const int FRAME = 0, VALUE = 1;
        protected Dictionary<int, State> LocalStates, RemoteStates;

        private const int FrameIntervalMs = 16;
        protected int LeastSimulatedFrame;

        public virtual void Start()
        {
            LocalStates = new Dictionary<int, State>();
            RemoteStates = new Dictionary<int, State>();
        }

        void FixedUpdate()
        {
            int frame = CurrentFrame;
            SetState.ApplyState();
            PredictionFrame(frame);
            LocalStates[frame].Simulate(true, 0);
            RemoteStates[frame].Simulate(false, 0);
            LeastSimulatedFrame = frame;
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

                int frameDelay = CurrentFrame - i;
                float startPlayTime = frameDelay * 1 / 60f; // FrameDeltaTime은 1/60f 등으로 정의돼 있어야 함

                RemoteStates[i].Simulate(false, startPlayTime);
                LeastSimulatedFrame = frame;
            }
        }

        private static int CalculateCurrentFrame()
        {
            long now = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            long correctedNow = now + TimeOffsetFromSharedStart;
            return (int)((correctedNow - SharedStartTimeMs) / FrameIntervalMs);
        }
    }
}