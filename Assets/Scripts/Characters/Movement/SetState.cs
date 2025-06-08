using System.Text;
using RollbackNetcode;
using UnityEngine;

namespace Movement
{
    public abstract class SetState
    {
        public abstract void ApplyState();
        protected abstract string StateSet();
        protected abstract void Initialize();

        protected string MessageFormatting(int type, string msg)
        {
            StringBuilder builder = new StringBuilder();
            builder
                .Append(type)
                .Append(Constant.SteamNetworkingType.DELIMITER)
                .Append(StateSimulator.CurrentFrame)
                .Append(Constant.SteamNetworkingType.DELIMITER)
                .Append(msg);
            return builder.ToString();
        }
    }
}