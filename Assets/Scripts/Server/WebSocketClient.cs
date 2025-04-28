using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Manager;
using RestSharp;
using UnityEngine;

namespace Server
{
    public class WebSocketClient : MonoBehaviour
    {
        private PingTest pingTest;
        private ClientWebSocket webSocket;
        private CancellationTokenSource cts;
        private string websocket_token;

        public async Task StartConnect()
        {
            try
            {
                pingTest = new PingTest();
                webSocket = new ClientWebSocket();
                cts = new CancellationTokenSource();

                await GetWebSocketToken();
                await webSocket.ConnectAsync(
                    new Uri(Constant.WEBSOCKET_URL(websocket_token, SteamNetworkManager.Manager.LocalSteamIdString)),
                    cts.Token);
                string response = await ReceiveMessageLoop();
                string jsonPart = response.Substring("PingTest:".Length);
                List<PingTestDTO> list = JsonSerializer.Deserialize<List<PingTestDTO>>(jsonPart);
                pingTest.Start(list);
                StartCoroutine(WaitPong());
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }

        private IEnumerator WaitPong()
        {
            yield return new WaitUntil(() => pingTest.IsReadDone);

            Debug.Log(pingTest.PingTestResult());
            SendMessage(pingTest.PingTestResult());
        }

        private async Task<string> ReceiveMessageLoop()
        {
            byte[] buffer = new byte[1024];

            while (webSocket.State == WebSocketState.Open)
            {
                try
                {
                    var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), cts.Token);
                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        Debug.Log("서버가 연결을 종료했습니다.");
                        await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, cts.Token);
                    }
                    else
                    {
                        string message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                        Debug.Log(message);
                        return message;
                    }
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                    break;
                }
            }

            return null;
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
                Debug.Log("WebSocket not connected");
                return;
            }

            byte[] bytes = Encoding.UTF8.GetBytes(message);
            ArraySegment<byte> segment = new ArraySegment<byte>(bytes);

            await webSocket.SendAsync(segment, WebSocketMessageType.Text, true, cts.Token);
        }
    }
    class WebSocketTokenDTO
    {
        public string webSocketToken { get; set; }
    }
}