using System;
using System.Collections.Generic;
using System.Text.Json;
using Manager;
using DTO;

namespace Server
{
    public static class PingTest
    {
        public static bool IsReadDone { get; set; }
        private static List<PingTestDTO> _idList;
        public static Dictionary<ulong, float> SentTime;
        public static Dictionary<ulong, float> ReceivedTime;
        private static int _sendId, _receiveId;

        public static void StartTest(string json)
        {
            SentTime = new Dictionary<ulong, float>();
            ReceivedTime = new Dictionary<ulong, float>();
            string jsonPart = json.Substring("PingTest:".Length);
            List<PingTestDTO> list = JsonSerializer.Deserialize<List<PingTestDTO>>(jsonPart);
            _idList = list;
            SendPing();
            _sendId = list.Count;
            _receiveId = 0;
        }

        public static void ReceivePingPong(ulong steamId, string receiveData)
        {
            switch (receiveData)
            {
                case "ping":
                    SteamNetworkManager.Manager.SendMsg(steamId, Constant.SteamNetworkingType.PINGTEST, "pong");
                    break;
                case "pong":
                    ReceivedTime.Add(steamId, DateTime.Now.Millisecond);
                    _receiveId++;
                    if (_receiveId == _sendId)
                    {
                        IsReadDone = true;
                    }
                    break;
            }
        }

        private static void SendPing()
        {
            IsReadDone = false;
            foreach (PingTestDTO id in _idList)
            {
                if (ulong.TryParse(id.Value, out ulong steamID))
                {
                    SteamNetworkManager.Manager.SendMsg(steamID, Constant.SteamNetworkingType.PINGTEST, "ping");
                    SentTime.Add(steamID, DateTime.Now.Millisecond);
                }
            }
        }
        
        public static string PingTestResult()
        {
            Dictionary<string, float> result = new Dictionary<string, float>();
            foreach (var pair in ReceivedTime)
            {
                ulong steamId = pair.Key;
                float ping = SentTime[steamId] - ReceivedTime[steamId];
                string key = FindKey(steamId.ToString());
                result.Add(key, ping);
            }

            return "PingResult:" + JsonSerializer.Serialize(result);
        }
        
        private static string FindKey(string steamId)
        {
            foreach (PingTestDTO dto in _idList)
            {
                if (dto.Value == steamId)
                {
                    return dto.Key;
                }
            }
            return null;
        }
    }
}