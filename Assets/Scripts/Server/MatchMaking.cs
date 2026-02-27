using System;
using System.Collections;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Manager;
using RestSharp;
using TMPro;
using UnityEngine;

namespace Server
{
    public class MatchMaking : MonoBehaviour
    {
        private ClientWebSocket webSocket;
        private CancellationTokenSource cts;
        private string websocket_token;

        //public RawImage image;
        private TextMeshProUGUI text;
        private float time = 0f;
        private bool isMatching = false;

        void Start()
        {
            DontDestroyOnLoad(gameObject);
            text = transform.GetComponentInChildren<TextMeshProUGUI>();
        }

        void Update()
        {
            if (isMatching)
            {
                time += Time.deltaTime;
                int minutes = (int)(time / 60);
                int seconds = (int)(time % 60);

                text.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
        }

        public async void StartMatching()
        {
            if (VarManager.Manager.PlayerCharacterName == null)
            {
                return;
            }

            isMatching = true;
            //image.gameObject.SetActive(false);
            //솔로 테스트 용

  /*          
            SteamNetworkManager.Manager.RemoteSteamId = SteamNetworkManager.Manager.PlayerSteamId;

            SteamNetworkManager.Manager.SendMsg(SteamNetworkManager.Manager.RemoteSteamId,
                Constant.SteamNetworkingType.CONNECTION,
                VarManager.Manager.PlayerCharacterName);
            isMatching = false;
            SceneLoadManager.Manager.LoadLoadingScene();
            
*/
            await StartConnect();
        }

        private async Task StartConnect()
        {
            try
            {
                webSocket = new ClientWebSocket();
                cts = new CancellationTokenSource();

                //WebSocket 연결
                await GetWebSocketToken();
                await webSocket.ConnectAsync(
                    new Uri(Constant.WEBSOCKET_URL(websocket_token, SteamNetworkManager.Manager.LocalSteamIdString)),
                    cts.Token);
                Print("Connect");

                string receiveData = await ReceiveMessageAsync();
                Debug.Log(receiveData);
                //서버에서 송신한 대기열 수신 및 핑테스트 진행
                PingTest.StartTest(receiveData);

                SceneLoadManager.Manager.LoadLoadingScene();

                StartCoroutine(WaitPong());

                //서버에서 송신한 상대 SteamID 설정
                SteamNetworkManager.Manager.RemoteSteamId = ulong.Parse(SplitMatchID(await ReceiveMessageAsync()));


                //상대에게 자신의 캐릭터 정보 전송 후 게임 시작 
                SteamNetworkManager.Manager.SendMsg(SteamNetworkManager.Manager.RemoteSteamId,
                    Constant.SteamNetworkingType.CONNECTION,
                    VarManager.Manager.PlayerCharacterName);
            }
            catch (Exception e)
            {
                Print(e.Message);
            }
        }

        private IEnumerator WaitPong()
        {
            yield return new WaitUntil(() => PingTest.IsReadDone);
            Print(PingTest.PingTestResult());
            yield return new WaitForTask(SendMessage(PingTest.PingTestResult()));
        }

        private string SplitMatchID(string response)
        {
            string split = response.Split(",")[0];
            return split.Substring("Match:".Length);
        }

        private async Task<string> ReceiveMessageAsync()
        {
            byte[] buffer = new byte[1024];

            try
            {
                WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), cts.Token);
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    Print("Server Closed Connection");
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, cts.Token);
                    return null;
                }

                return Encoding.UTF8.GetString(buffer, 0, result.Count);
            }
            catch (Exception e)
            {
                Print(e.Message);
                return null;
            }
        }

        private async Task GetWebSocketToken()
        {
            RestResponse response = await RestAPIRequest.Post<string>(Constant.RestAPI.Auth.WEBSOCKET_TOKEN, null,
                LoginManager.Manager.GetAuthHeader());
            if (response.IsSuccessful)
            {
                websocket_token = JsonSerializer.Deserialize<WebSocketTokenDTO>(response.Content).webSocketToken;
            }
        }

        public async Task SendMessage(string message)
        {
            if (webSocket.State != WebSocketState.Open)
            {
                Print("Not Connected");
                return;
            }

            byte[] bytes = Encoding.UTF8.GetBytes(message);
            ArraySegment<byte> segment = new ArraySegment<byte>(bytes);

            await webSocket.SendAsync(segment, WebSocketMessageType.Text, true, cts.Token);
            Print("Waiting Response...");
        }


        async void OnDestroy()
        {
            await StopMatching();
        }

        async void OnApplicationQuit()
        {
            await StopMatching();
        }

        private async Task StopMatching()
        {
            Debug.Log("Break WebSocket");
            if (webSocket.State == WebSocketState.Open || webSocket.State == WebSocketState.Connecting)
            {
                try
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
                    Print("Closed");
                }
                catch (WebSocketException wse)
                {
                    Print($"WebSocketException during close: {wse.Message}");
                }
                catch (Exception ex)
                {
                    Print($"Unexpected error during WebSocket close: {ex.Message}");
                }
                finally
                {
                    webSocket.Dispose();
                    webSocket = null;
                }
            }

            cts.Cancel();
            cts.Dispose();
            cts = null;
        }

        private void Print(string message)
        {
            Debug.Log("[WebSocket] > " + message);
        }
    }

    class WebSocketTokenDTO
    {
        public string webSocketToken { get; set; }
    }

    public class WaitForTask : CustomYieldInstruction
    {
        private Task task;
        public override bool keepWaiting => !task.IsCompleted;

        public WaitForTask(Task task)
        {
            this.task = task;
        }
    }
}