using System;
using System.Collections.Generic;
using RollbackNetcode.SetState;
using UnityEngine;
using RollbackNetcode.State;

namespace RollbackNetcode.StateSimulator
{
    public abstract class StateSimulatorBase : MonoBehaviour
    {
        protected SetStateBase SetStateBase { get; set; }
        public static int CurrentFrame => CalculateCurrentFrame();
        public static long SharedStartTimeMs;
        public static long TimeOffsetFromSharedStart;

        protected const int FRAME = 0, VALUE = 1;
        protected Dictionary<int, StateBase> LocalStates, RemoteStates;

        private const int FrameIntervalMs = 16;
        protected int LeastSimulatedFrame;

        public virtual void Start()
        {
            LocalStates = new Dictionary<int, StateBase>();
            RemoteStates = new Dictionary<int, StateBase>();
        }

        void FixedUpdate()
        {
            int frame = CurrentFrame;
            SetStateBase.ApplyState();
            PredictionFrame(frame);
            LocalStates[frame].Simulate(true, 0);
            RemoteStates[frame].Simulate(false, 0);
            LeastSimulatedFrame = frame;
        }

        public void AddState(int frame, StateBase stateBase)
        {
            LocalStates[frame] = stateBase;
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