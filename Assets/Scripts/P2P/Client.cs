/*
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

namespace P2P
{
    public class Client : MonoBehaviour
    {
        private static OpponentInfo opponentInfo;
        private TcpClient tcpClient;
        private static Thread thread;

        public static void WaitMatching()
        {
            thread = new Thread(TcpConnect);
            thread.IsBackground = true;
            thread.Start();
        }

        public static void EndMatching()
        {
            thread.Abort();
        }

        static void TcpConnect()
        {
            Debug.Log("Client thread start");
            TcpClient tcpClient = new TcpClient();
            IPEndPoint ipEnd =
                new IPEndPoint(IPAddress.Parse(Constant.SERVER_IP), Constant.SERVER_PORT);
            tcpClient.Connect(ipEnd);

            NetworkStream stream = tcpClient.GetStream();
            byte[] buffer = new byte[1024];
            Debug.Log("Waiting...");
            int readByte = stream.Read(buffer, 0, buffer.Length);
            Debug.Log("Matching");
            string receivedData = Encoding.UTF8.GetString(buffer, 0, readByte);
            opponentInfo = new OpponentInfo(receivedData.Split(","));
            Debug.Log("Client thread end");
        }

        public static OpponentInfo GetOpponentInfo()
        {
            return opponentInfo;
        }

        public static Thread GetThread()
        {
            return thread;
        }
    }
}*/