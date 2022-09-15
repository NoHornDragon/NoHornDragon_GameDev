using UnityEngine;

namespace NHD.Multiplay.ServerSide
{
    public class ServerHandle
    {
        public static void WelcomeReceived(int _fromClient, Packet _packet)
        {
            int clientIdCheck = _packet.ReadInt();
            string username = _packet.ReadString();

            Debug.Log($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected success" +
            $"and now player is {_fromClient}");
            if (_fromClient != clientIdCheck)
            {
                Debug.Log($"Player \"{username}\" (ID: {_fromClient}) has assumed the wrong client id {clientIdCheck}!");
            }

            Server.clients[_fromClient].SendIntoGame(username);
        }

        public static void PlayerMovement(int _fromClient, Packet _packet)
        {
            Vector3 position = _packet.ReadVector3();
            Quaternion rotation = _packet.ReadQuaternion();

            Server.clients[_fromClient].player.SetPosition(position, rotation);
        }
    }
}