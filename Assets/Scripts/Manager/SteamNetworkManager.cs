using System;
using System.Text;
using RollbackNetcode;
using Server;
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

        public ulong RemoteSteamId { get; set; }
        //public string RemoteSteamIdString { get; set; }


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

        void FixedUpdate()
        {
            if (SteamNetworking.IsP2PPacketAvailable())
            {
                var packet = SteamNetworking.ReadP2PPacket();

                if (packet.HasValue)
                {
                    string receiveData = Encoding.UTF8.GetString(packet.Value.Data);
                    //SteamNetworkingType>Data
                    string[] splitData = receiveData.Split(Constant.SteamNetworkingType.DELIMITER);
                    switch (int.Parse(splitData[0]))
                    {
                        //splitData[1] = ping | pong
                        case Constant.SteamNetworkingType.PINGTEST:
                            PingTest.ReceivePingPong(packet.Value.SteamId, splitData[1]);
                            break;
                        //splitData[1] = CharacterName
                        case Constant.SteamNetworkingType.CONNECTION:
                            //상대 캐릭터 정보 설정 후 SceneLoad
                            SceneLoadManager.Manager.LoadGameScene(splitData[1]);
                            break;
                        //splitData[1] = KeyInputType>CurrentFrame>Data
                        case Constant.SteamNetworkingType.KEYINPUT:
                            RollbackManager.Manager.ProcessingMessage(receiveData.Substring("2>".Length));
                            break;
                        case Constant.SteamNetworkingType.SYNC_TIME:
                        {
                            long remoteTime = long.Parse(splitData[1]);
                            long myNow = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                            SendMsg(packet.Value.SteamId.Value, Constant.SteamNetworkingType.SYNC_RESPONSE,
                                myNow.ToString());
                            break;
                        }
                        case Constant.SteamNetworkingType.SYNC_RESPONSE:
                        {
                            long remoteTime = long.Parse(splitData[1]);
                            RollbackManager.Manager.OnReceiveTimeSyncResponse(remoteTime);
                            break;
                        }
                        case Constant.SteamNetworkingType.END_GAME:
                        {
                            StartCoroutine(GameManager.Manager.EndGame(bool.Parse(splitData[1])));
                            break;
                        }
                    }
                }
            }
        }

        public bool SendMsg(ulong steamId, int type, string msg)
        {
            if (!SteamClient.IsValid)
            {
                Debug.LogError("[Steam] Steam 클라이언트가 유효하지 않습니다!");
                return false;
            }

            byte[] data = Encoding.UTF8.GetBytes(type.ToString() + Constant.SteamNetworkingType.DELIMITER + msg);
            return SteamNetworking.SendP2PPacket(steamId, data);
        }
    }
}