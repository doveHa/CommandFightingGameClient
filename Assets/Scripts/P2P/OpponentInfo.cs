/*namespace P2P
{
    public class OpponentInfo
    {
        private string ipAddress, selectedCharacter;
        private int localPort;
        private int opponentPort;
        private bool isHost;

        public OpponentInfo(string[] networkData)
        {
            isHost = networkData[Constant.ISHOST].Equals("host");
            ipAddress = networkData[Constant.IP_ADDRESS].Trim();
            if (isHost)
            {
                localPort = int.Parse(networkData[Constant.PORT].Trim());
                opponentPort = localPort + 1;
            }
            else
            {
                opponentPort = int.Parse(networkData[Constant.PORT].Trim());
                localPort = opponentPort + 1;
            }
        }

        public string GetIpAddress()
        {
            return ipAddress;
        }

        public int GetLocalPort()
        {
            return localPort;
        }

        public int GetOpponentPort()
        {
            return opponentPort;
        }

        public bool IsHost()
        {
            return isHost;
        }

        public string toString()
        {
            return ipAddress + ", " + localPort + ", " + opponentPort + ", " + isHost;
        }

        public void SetCharacter(string character)
        {
            selectedCharacter = character;
        }
        
        public string GetSelectedCharacter()
        {
            return selectedCharacter;
        }
    }
}*/