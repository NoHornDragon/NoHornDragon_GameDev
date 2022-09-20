using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

namespace NHD.Multiplay.ClientSide
{
    public class Client : MonoBehaviour
    {
        public static Client _instance;
        public static int _dataBufferSize = 4096;

        public string _ip = "127.0.0.1";
        public int _port = 26950;
        public int _myId = 0;
        public TCP _tcp;
        public UDP _udp;
        private bool _isConnected = false;

        private delegate void PacketHandler(Packet packet);
        private static Dictionary<int, PacketHandler> _packetHandlers;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else if (_instance != this)
            {
                Debug.Log($"instance already exist, so destroy object!");
                Destroy(this);
            }
        }

        private void OnApplicationQuit()
        {
            Disconnect();
        }

        private void Start()
        {
            _tcp = new TCP();
            _udp = new UDP();
        }

        public void ConnectToServer()
        {
            InitializeClientData();

            _isConnected = true;
            _tcp.Connect();
        }

        public class TCP
        {
            public TcpClient socket;
            private NetworkStream stream;
            private Packet receivedData;
            private byte[] receiveBuffer;

            public void Connect()
            {
                socket = new TcpClient
                {
                    ReceiveBufferSize = _dataBufferSize,
                    SendBufferSize = _dataBufferSize
                };

                receiveBuffer = new byte[_dataBufferSize];
                socket.BeginConnect(_instance._ip, _instance._port, ConnectCallback, socket);
            }

            private void ConnectCallback(IAsyncResult result)
            {
                socket.EndConnect(result);

                if (!socket.Connected)
                {
                    return;
                }

                stream = socket.GetStream();

                receivedData = new Packet();

                stream.BeginRead(receiveBuffer, 0, _dataBufferSize, ReceiveCallback, null);
            }

            private void ReceiveCallback(IAsyncResult result)
            {
                try
                {
                    int byteLength = stream.EndRead(result);
                    if (byteLength <= 0)
                    {
                        _instance.Disconnect();
                        return;
                    }

                    byte[] data = new byte[byteLength];
                    Array.Copy(receiveBuffer, data, byteLength);

                    receivedData.Reset(HandleData(data));
                    stream.BeginRead(receiveBuffer, 0, _dataBufferSize, ReceiveCallback, null);
                }
                catch
                {
                    Disconnected();
                }
            }

            public void SendData(Packet packet)
            {
                try
                {
                    if (socket != null)
                    {
                        stream.BeginWrite(packet.ToArray(), 0, packet.Length(), null, null);
                    }
                }
                catch (Exception ex)
                {
                    Debug.Log($"Error sending data to server via TCP : {ex}");
                }
            }

            private bool HandleData(byte[] data)
            {
                int packetLength = 0;

                receivedData.SetBytes(data);

                if (receivedData.UnreadLength() >= 4)
                {
                    packetLength = receivedData.ReadInt();
                    if (packetLength <= 0)
                    {
                        return true;
                    }
                }

                while (packetLength > 0 && packetLength <= receivedData.UnreadLength())
                {
                    byte[] packetBytes = receivedData.ReadBytes(packetLength);
                    ThreadManager.ExecuteOnMainThread(() =>
                    {
                        using (Packet packet = new Packet(packetBytes))
                        {
                            int packetId = packet.ReadInt();
                            _packetHandlers[packetId](packet);
                        }
                    });

                    packetLength = 0;
                    if (receivedData.UnreadLength() >= 4)
                    {
                        packetLength = receivedData.ReadInt();
                        if (packetLength <= 0)
                        {
                            return true;
                        }
                    }
                }

                if (packetLength <= 1)
                    return true;
                return false;
            }

            private void Disconnected()
            {
                _instance.Disconnect();

                stream = null;
                receiveBuffer = null;
                receivedData = null;
                socket = null;
            }
        }


        public class UDP
        {
            public UdpClient socket;
            public IPEndPoint endPoint;

            public UDP()
            {
                endPoint = new IPEndPoint(IPAddress.Parse(_instance._ip), _instance._port);
            }

            public void Connect(int localPort)
            {
                socket = new UdpClient(localPort);
                socket.Connect(endPoint);

                socket.BeginReceive(ReceiveCallback, null);

                using (Packet packet = new Packet())
                {
                    SendData(packet);
                }
            }

            public void SendData(Packet packet)
            {
                try
                {
                    packet.InsertInt(_instance._myId);
                    if (socket != null)
                    {
                        socket.BeginSend(packet.ToArray(), packet.Length(), null, null);
                    }
                }
                catch (Exception ex)
                {
                    Debug.Log($"Error sending data to server via UDP : {ex}");
                }
            }

            private void ReceiveCallback(IAsyncResult result)
            {
                try
                {
                    byte[] data = socket.EndReceive(result, ref endPoint);
                    socket.BeginReceive(ReceiveCallback, null);

                    if (data.Length < 4)
                    {
                        _instance.Disconnect();
                        return;
                    }

                    HandleData(data);
                }
                catch
                {
                    Disconnect();
                }
            }

            private void HandleData(byte[] data)
            {
                using (Packet packet = new Packet(data))
                {
                    int packetLength = packet.ReadInt();
                    data = packet.ReadBytes(packetLength);
                }

                ThreadManager.ExecuteOnMainThread(() =>
                {
                    using (Packet packet = new Packet(data))
                    {
                        int packetId = packet.ReadInt();
                        _packetHandlers[packetId](packet);
                    }
                });
            }

            private void Disconnect()
            {
                _instance.Disconnect();

                endPoint = null;
                socket = null;
            }
        }

        private void InitializeClientData()
        {
            _packetHandlers = new Dictionary<int, PacketHandler>()
        {
            { (int)ServerPackets.welcome, ClientHandle.Welcome },
            { (int)ServerPackets.spawnPlayer, ClientHandle.SpawnPlayer },
            { (int)ServerPackets.playerPosition, ClientHandle.PlayerPosition },
            { (int)ServerPackets.playerEmoji, ClientHandle.PlayerEmoji },
            { (int)ServerPackets.playerDisconnected, ClientHandle.PlayerDisconnected }
        };

            Debug.Log($"Initialized packets.");
        }

        private void Disconnect()
        {
            if (!_isConnected) return;

            _isConnected = false;
            _tcp.socket.Close();
            _udp.socket.Close();

            Debug.Log($"Disconnected from server");
        }
    }
}