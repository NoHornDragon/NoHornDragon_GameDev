using UnityEngine;

namespace NHD.Multiplay.ServerSide
{
    public class ServerHandle
    {
        public static void WelcomeReceived(int fromClient, Packet packet)
        {
            int clientIdCheck = packet.ReadInt();
            string username = packet.ReadString();

            Debug.Log($"{Server._clients[fromClient]._tcp.socket.Client.RemoteEndPoint} connected success" +
            $"and now player is {fromClient}");
            if (fromClient != clientIdCheck)
            {
                Debug.Log($"Player \"{username}\" (ID: {fromClient}) has assumed the wrong client id {clientIdCheck}!");
            }

            Server._clients[fromClient].SendIntoGame(username);
        }

        public static void PlayerMovement(int fromClient, Packet packet)
        {
            Vector3 position = packet.ReadVector3();
            Quaternion rotation = packet.ReadQuaternion();

            Server._clients[fromClient]._player.SetPosition(position, rotation);
        }

        public static void PlayerEmoji(int fromClient, Packet packet)
        {
            int emojiIndex = packet.ReadInt();
            // Debug.Log($"[{emojiIndex}] : Emoji Get from client to server");
            Server._clients[fromClient]._player.TriggerEmoji(emojiIndex);
        }
    }
}