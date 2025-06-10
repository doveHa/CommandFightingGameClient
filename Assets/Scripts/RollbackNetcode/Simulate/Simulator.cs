using System.Collections.Generic;
using Manager;
using UnityEngine;

namespace RollbackNetcode
{
    public class Simulator
    {
        private bool IsLocal { get; set; }

        public Dictionary<int, State> ActiveStates;
        public Dictionary<int, State> JumpStates;
        public Dictionary<int, State> MoveStates;

        public Simulator(bool isLocal)
        {
            IsLocal = isLocal;

            ActiveStates = new Dictionary<int, State>();
            JumpStates = new Dictionary<int, State>();
            MoveStates = new Dictionary<int, State>();
        }

        public void Simulate(int frame, float startTime)
        {
            ActiveStates[frame].Simulate(IsLocal, startTime);
            JumpStates[frame].Simulate(IsLocal, startTime);
            MoveStates[frame].Simulate(IsLocal, startTime);
            //Debug.Log($"[{frame}] Simulation End");
        }
    }

    public abstract class State
    {
        public virtual void Print()
        {
        }

        public abstract void Simulate(bool isLocal, float startTime);
        public abstract State Clone();
    }
}