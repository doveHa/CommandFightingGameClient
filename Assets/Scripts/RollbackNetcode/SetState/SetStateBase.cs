using System.Text;
using RollbackNetcode;
using RollbackNetcode.State;
using RollbackNetcode.StateSimulator;

namespace RollbackNetcode.SetState
{
    public abstract class SetStateBase
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
                .Append(StateSimulatorBase.CurrentFrame)
                .Append(Constant.SteamNetworkingType.DELIMITER)
                .Append(msg);
            return builder.ToString();
        }
    }
}