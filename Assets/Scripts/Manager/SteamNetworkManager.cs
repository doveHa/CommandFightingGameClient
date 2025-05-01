using System;
using System.Collections;
using System.Text;
using UnityEngine;
using Steamworks;

namespace Manager
{
    public class SteamNetworkManager : MonoBehaviour
    {
        public static SteamNetworkManager Manager { get; private set; }

        private static uint gameAppId = 480;
        public SteamId PlayerSteamId { get; private set; }
        public string LocalSteamIdString { get; set; }
        public string RemoteSteamIdString { get; set; }


        void Awake()
        {
            if (Manager == null)
            {
                DontDestroyOnLoad(gameObject);
                Manager = this;

                try
                {
                    SteamClient.Init(gameAppId);

                    if (!SteamClient.IsValid)
                    {
                        Debug.LogError("Steam 클라이언트가 유효하지 않습니다!");
                        throw new Exception();
                    }

                    PlayerSteamId = SteamClient.SteamId;
                    Print(PlayerSteamId.ToString());

                    LocalSteamIdString = PlayerSteamId.ToString();

                    SteamNetworking.OnP2PSessionRequest = (steamId) =>
                    {
                        SteamNetworking.AcceptP2PSessionWithUser(steamId);
                    };
                }
                catch (Exception e)
                {
                    Debug.LogError($"[Steam] 초기화 실패: {e.Message}");
                }
            }
        }

        public void StartP2P()
        {
            SendMsg(Constant.SteamNetworkingType.CONNECTION, CharacterManager.Manager.PlayerCharacterName);
            StartCoroutine(OpponentCharacter());
        }

        IEnumerator OpponentCharacter()
        {
            Print("Waiting for Packet");
            yield return new WaitUntil(() => SteamNetworking.IsP2PPacketAvailable());
            Print("Received P2P Packet");
            var packet = SteamNetworking.ReadP2PPacket();

            if (packet.HasValue)
            {
                string receivedMessage = Encoding.UTF8.GetString(packet.Value.Data);
                CharacterManager.Manager.OpponentCharacterName = receivedMessage;
                Print($"{packet.Value.SteamId} 로부터 메시지 수신: {receivedMessage}");

                SceneLoadManager.Manager.LoadGameScene();
            }
        }

        public void SendTestMessage()
        {
            byte[] data = Encoding.UTF8.GetBytes("TestMessage");
            //ulong targetSteamId = 76561198853166461;
            ulong targetSteamId = 76561199834491206;
            bool result = SteamNetworking.SendP2PPacket(targetSteamId, data);
            Print(result.ToString());
        }

        void Update()
        {
            while (SteamNetworking.IsP2PPacketAvailable())
            {
                var packet = SteamNetworking.ReadP2PPacket();

                if (packet.HasValue)
                {
                    string receivedMessage = Encoding.UTF8.GetString(packet.Value.Data);
                    CharacterManager.Manager.OpponentCharacterName = receivedMessage;
                    Print($"{packet.Value.SteamId} 로부터 메시지 수신: {receivedMessage}");
                }
            }
        }

        public void SendMsg(int type, string msg)
        {
            if (!SteamClient.IsValid)
            {
                Debug.LogError("[Steam] Steam 클라이언트가 유효하지 않습니다!");
                return;
            }

            byte[] data = Encoding.UTF8.GetBytes(msg);
            ulong targetSteamId = ulong.Parse(RemoteSteamIdString);
            bool result = SteamNetworking.SendP2PPacket(targetSteamId, data);
            Print(result.ToString());
        }


        private void OnDestroy()
        {
            SteamClient.Shutdown();
            Print("Steam Closed");
        }

        private void Print(string message)
        {
            Debug.Log("[SteamNetworkManager] > " + message);
        }
    }
}