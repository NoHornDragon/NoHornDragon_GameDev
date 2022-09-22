using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

namespace NHD.Multiplay.ServerSide
{
    public class Server
    {
        public static int _maxPlayers { get; private set; }
        public static int _port { get; private set; }

        public static Dictionary<int, ClientInfo> _clients = new Dictionary<int, ClientInfo>();

        public delegate void PacketHandler(int fromClient, Packet packet);
        public static Dictionary<int, PacketHandler> _packetHandlers;

        public static TcpListener _tcpListener;
        public static UdpClient _udpListener;

        public static void Start(int maxPlayer, int port)
        {
            _maxPlayers = maxPlayer;
            _port = port;

            Debug.Log($"Starting server...");

            InitializeServerData();

            _tcpListener = new TcpListener(IPAddress.Any, _port);
            _tcpListener.Start();
            _tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallback), null);

            _udpListener = new UdpClient(_port);
            _udpListener.BeginReceive(UDPReceiveCallback, null);

            Debug.Log($"Server started on {_port}");
        }

        private static void TCPConnectCallback(IAsyncResult result)
        {
            TcpClient client = _tcpListener.EndAcceptTcpClient(result);
            _tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallback), null);

            Debug.Log($"Incoming connection from {client.Client.RemoteEndPoint}...");
            for (int i = 1; i <= _maxPlayers; i++)
            {
                if (_clients[i]._tcp.socket == null)
                {
                    _clients[i]._tcp.Connect(client);
                    return;
                }
            }

            Debug.Log($"{client.Client.RemoteEndPoint} failed to connect : Server full!");
        }


        private static void UDPReceiveCallback(IAsyncResult result)
        {
            try
            {
                IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = _udpListener.EndReceive(result, ref clientEndPoint);
                _udpListener.BeginReceive(UDPReceiveCallback, null);

                if (data.Length < 4)
                {
                    return;
                }

                using (Packet packet = new Packet(data))
                {
                    int clientId = packet.ReadInt();

                    if (clientId == 0) return;

                    if (_clients[clientId]._udp.endPoint == null)
                    {
                        _clients[clientId]._udp.Connect(clientEndPoint);
                        return;
                    }

                    if (_clients[clientId]._udp.endPoint.ToString() == clientEndPoint.ToString())
                    {
                        _clients[clientId]._udp.HandleData(packet);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Log($"Error receiving UDP data : {ex}");
            }
        }

        public static void SendUDPData(IPEndPoint clientEndPoint, Packet packet)
        {
            try
            {
                if (clientEndPoint != null)
                {
                    _udpListener.BeginSend(packet.ToArray(), packet.Length(), clientEndPoint, null, null);
                }
            }
            catch (Exception ex)
            {
                Debug.Log($"Error sending data to {clientEndPoint} via UDP : {ex}");
            }
        }

        private static void InitializeServerData()
        {
            for (int i = 1; i <= _maxPlayers; i++)
            {
                _clients.Add(i, new ClientInfo(i));
            }

            _packetHandlers = new Dictionary<int, PacketHandler>()
        {
            {(int)ClientPackets.welcomeReceived, ServerHandle.WelcomeReceived },
            {(int)ClientPackets.playerMovement, ServerHandle.PlayerMovement },
            {(int)ClientPackets.playerEmoji, ServerHandle.PlayerEmoji }
        };
            Debug.Log($"Initialize packets");
        }

        public static void Stop()
        {
            _tcpListener.Stop();
            _udpListener.Close();
        }
    }
}