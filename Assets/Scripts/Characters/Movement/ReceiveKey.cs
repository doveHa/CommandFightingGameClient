using System.Collections;
using System.Collections.Generic;
using System.Text;
using RollbackNetCode;
using Steamworks;
using UnityEngine;

public class ReceiveKey : MonoBehaviour
{
    void Update()
    {
        if (SteamNetworking.IsP2PPacketAvailable())
        {
            var packet = SteamNetworking.ReadP2PPacket();
            if (packet.HasValue)
            {
                string receiveData = Encoding.UTF8.GetString(packet.Value.Data);
                string[] input = receiveData.Split(' ');
                int frame = int.Parse(input[0]);
                int locate = -1 * int.Parse(input[1]);

                RollbackManager.Manager.inputDictionary.RemoteInput[frame] = locate;
            }
        }
    }
}