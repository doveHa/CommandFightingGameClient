using System.Collections.Generic;
using Manager;
using UnityEngine;

namespace RollbackNetcode
{
    public class Simulator
    {
        public Dictionary<int, State> PlayerStates, OpponentStates;

        public Simulator()
        {
            PlayerStates = new Dictionary<int, State>();
            OpponentStates = new Dictionary<int, State>();
        }

        public void Simulate(int frame)
        {
            PlayerStates[frame].Run(true);
            OpponentStates[frame].Run(false);
        }
    }

    public abstract class State
    {
        public abstract void Run(bool isPlayer);
    }
}