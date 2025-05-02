using System;
using Steamworks;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UnityEngine;

namespace Server
{
    public class PingTest
    {
        public bool IsReadDone { get; private set; }
        private List<PingTestDTO> IdList;
        Dictionary<ulong, float> sentTime = new Dictionary<ulong, float>();
        Dictionary<ulong, float> receivedTime = new Dictionary<ulong, float>();

        public void Start(string json)
        {
            string jsonPart = json.Substring("PingTest:".Length);
            List<PingTestDTO> list = JsonSerializer.Deserialize<List<PingTestDTO>>(jsonPart);

            IdList = list;
            Task.Run(Ping);
            Task.Run(Pong);
        }

        private void Ping()
        {
            IsReadDone = false;
            foreach (PingTestDTO id in IdList)
            {
                if (ulong.TryParse(id.Value, out ulong steamID))
                {
                    byte[] data = Encoding.UTF8.GetBytes("ping");

                    if (SteamNetworking.SendP2PPacket(steamID, data))
                    {
                        Print("Send Ping");
                        sentTime.Add(steamID, DateTime.Now.Millisecond);
                    }
                }
            }
        }

        private void Pong()
        {
            while (!IsReadDone)
            {
                if (SteamNetworking.IsP2PPacketAvailable())
                {
                    var packet = SteamNetworking.ReadP2PPacket();
                    if (packet.HasValue)
                    {
                        switch (Encoding.UTF8.GetString(packet.Value.Data))
                        {
                            case "ping":
                                SteamNetworking.SendP2PPacket(packet.Value.SteamId, Encoding.UTF8.GetBytes("pong"));
                                Print("Send Pong");
                                break;
                            case "pong":
                                receivedTime.Add(packet.Value.SteamId, DateTime.Now.Millisecond);
                                Print("Received Pong");
                                IsReadDone = true;
                                break;
                        }
                    }
                }
            }
        }

        private string FindKey(string steamId)
        {
            foreach (PingTestDTO dto in IdList)
            {
                if (dto.Value == steamId)
                {
                    return dto.Key;
                }
            }

            return null;
        }

        public string PingTestResult()
        {
            Dictionary<string, float> result = new Dictionary<string, float>();
            foreach (var pair in receivedTime)
            {
                ulong steamId = pair.Key;
                float ping = receivedTime[steamId] - sentTime[steamId];
                string key = FindKey(steamId.ToString());
                result.Add(key, ping);
            }

            return "PingResult:" + JsonSerializer.Serialize(result);
        }

        public void Print(string message)
        {
            Debug.Log("[PingTest] > " + message);
        }

        private class PingTestDTO
        {
            public string Key { get; set; }
            public string Value { get; set; }
        }
    }
}