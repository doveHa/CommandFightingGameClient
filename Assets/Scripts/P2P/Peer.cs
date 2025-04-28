/*
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

namespace P2P
{
    class Peer : MonoBehaviour
    {
        private static Thread P2Pthread;
        private static UdpClient udpClient;
        private static OpponentInfo opponent;
        private static int localPort;
        private static int opponentPort;
        public static bool isP2PConnect;
        private static Queue<string> sendQueue;

        void Start()
        {
            DontDestroyOnLoad(this);
            sendQueue = new Queue<string>();
        }

        public static void P2PMatchingStart()
        {
            P2Pthread = new Thread(threadStart);
            P2Pthread.IsBackground = true;
            P2Pthread.Start();
        }

        public static void EndMatching()
        {
            P2Pthread.Abort();
        }

        static void threadStart()
        {
            Debug.Log("P2P thread start");
            Client.GetThread().Join();
            Debug.Log("Client thread join");

            opponent = Client.GetOpponentInfo();
            udpClient = new UdpClient(opponent.GetLocalPort());
            Debug.Log(opponent.toString());
            //udpClient.Connect(opponent.GetIpAddress(), opponent.GetPort());

            Thread sendThread = new Thread(SendMessage);
            sendThread.IsBackground = true;
            sendThread.Start();

            Thread receiveThread = new Thread(ReceiveMessage);
            receiveThread.IsBackground = true;
            receiveThread.Start();

            isP2PConnect = true;
        }
        
        static void SendMessage()
        {
            while (true)
            {
                if (sendQueue.TryPeek(out string msg))
                {
                    udpClient.Send(Encoding.UTF8.GetBytes(msg), msg.Length,
                        opponent.GetIpAddress(),
                        opponent.GetOpponentPort());
                    Debug.Log("send");
                }
            }
        }

        static void ReceiveMessage()
        {
            while (true)
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = udpClient.Receive(ref endPoint);
                string[] receiveData = Encoding.UTF8.GetString(data).Split(":");
                ReceiveDataTreatment.TreatMentReceiveData(receiveData);
            }
        }

        public static void SendMsg(int type, string msg)
        {
            sendQueue.Enqueue(type + ":" + msg);
        }
        
        public static OpponentInfo GetOpponent()
        {
            return opponent;
        }

        
    }
}*/