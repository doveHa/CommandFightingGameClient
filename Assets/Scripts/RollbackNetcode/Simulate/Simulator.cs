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

        public void Simulate(int frame)
        {
            ActiveStates[frame].Run(IsLocal);
            JumpStates[frame].Run(IsLocal);
            MoveStates[frame].Run(IsLocal);
            //Debug.Log($"[{frame}] Simulation End");
        }
    }

    public abstract class State
    {
        public virtual void Print()
        {
            
        }
        public abstract void Run(bool isLocal);
        public abstract State Clone();
    }
}