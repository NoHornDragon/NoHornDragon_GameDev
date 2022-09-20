using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

namespace NHD.Multiplay.ServerSide
{
    public class ClientInfo
    {
        public static int _dataBuferSize = 4096;
        public int _id;

        public PlayerTrackerInServer _player;

        public TCP _tcp;
        public UDP _udp;

        public ClientInfo(int clientId)
        {
            _id = clientId;
            _tcp = new TCP(_id);
            _udp = new UDP(_id);
        }

        public class TCP
        {
            public TcpClient socket;

            private readonly int id;
            private Packet receivedData;
            private NetworkStream stream;
            private byte[] receiveBuffer;

            public TCP(int inputId)
            {
                id = inputId;
            }

            public void Connect(TcpClient inputSocket)
            {
                socket = inputSocket;
                socket.ReceiveBufferSize = _dataBuferSize;
                socket.SendBufferSize = _dataBuferSize;

                stream = socket.GetStream();

                receivedData = new Packet();
                receiveBuffer = new byte[_dataBuferSize];

                stream.BeginRead(receiveBuffer, 0, _dataBuferSize, ReceiveCallback, null);

                // send welcome packet
                ServerSend.Welcome(id, "Welcome to NoHornDragon server");
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
                    Debug.Log($"Error Sending data to player {id} via TCP : {ex}");
                }
            }


            private void ReceiveCallback(IAsyncResult result)
            {
                try
                {
                    int byteLength = stream.EndRead(result);
                    if (byteLength <= 0)
                    {
                        Server._clients[id].Disconnect();
                        return;
                    }

                    byte[] data = new byte[byteLength];
                    Array.Copy(receiveBuffer, data, byteLength);

                    // handle data
                    receivedData.Reset(HandleData(data));
                    stream.BeginRead(receiveBuffer, 0, _dataBuferSize, ReceiveCallback, null);
                }
                catch (Exception ex)
                {
                    Debug.Log($"Error receiving TCP data : {ex}");
                    Server._clients[id].Disconnect();

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
                            Server._packetHandlers[packetId](id, packet);
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

            public void Disconnect()
            {
                socket.Close();
                stream = null;
                receiveBuffer = null;
                receivedData = null;
                socket = null;
            }
        }


        public class UDP
        {
            public IPEndPoint endPoint;
            private int id;

            public UDP(int inputId)
            {
                id = inputId;
            }

            public void Connect(IPEndPoint ipEndPoint)
            {
                endPoint = ipEndPoint;
            }

            public void SendData(Packet packet)
            {
                Server.SendUDPData(endPoint, packet);
            }

            public void HandleData(Packet packetData)
            {
                int packetLength = packetData.ReadInt();
                byte[] packetBytes = packetData.ReadBytes(packetLength);

                ThreadManager.ExecuteOnMainThread(() =>
                {
                    using (Packet packet = new Packet(packetBytes))
                    {
                        int packetId = packet.ReadInt();
                        Server._packetHandlers[packetId](id, packet);
                    }
                });
            }

            public void Disconnect()
            {
                endPoint = null;
            }
        }

        public void SendIntoGame(string _playerName)
        {
            _player = NetworkManager._instance.InstantiatePlayer();
            _player.Initialize(_id, _playerName);

            foreach (ClientInfo _client in Server._clients.Values)
            {
                if (_client._player != null)
                {
                    if (_client._id != _id)
                    {
                        ServerSend.SpawnPlayer(_id, _client._player);
                    }
                }
            }

            // send new player info to all the other player
            foreach (ClientInfo _client in Server._clients.Values)
            {
                if (_client._player != null)
                {
                    ServerSend.SpawnPlayer(_client._id, _player);

                }
            }
        }

        private void Disconnect()
        {
            Debug.Log($"{_tcp.socket.Client.RemoteEndPoint} has disconnected");

            ThreadManager.ExecuteOnMainThread(() =>
            {
                UnityEngine.Object.Destroy(_player.gameObject);
                _player = null;
            });

            _tcp.Disconnect();
            _udp.Disconnect();

            ServerSend.PlayerDisconnected(_id);
        }

    }
}