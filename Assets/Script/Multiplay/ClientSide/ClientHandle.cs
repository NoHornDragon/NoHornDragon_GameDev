using System.Net;
using UnityEngine;

namespace NHD.Multiplay.ClientSide
{
    // Server에서 받은 패킷을 처리하는 곳
    // Server에서 보낸 패킷 형태와 같은지 확인합시당
    public class ClientHandle : MonoBehaviour
    {
        public static void Welcome(Packet pakcet)
        {
            string msg = pakcet.ReadString();
            int myId = pakcet.ReadInt();

            Debug.Log($"Message from server : {msg}");
            Client.instance.myId = myId;

            // send welcome received packet
            ClientSend.WelcomeReceived();

            Client.instance.udp.Connect(((IPEndPoint)Client.instance.tcp.socket.Client.LocalEndPoint).Port);
        }

        public static void SpawnPlayer(Packet pakcet)
        {
            int id = pakcet.ReadInt();
            string username = pakcet.ReadString();
            Vector3 position = pakcet.ReadVector3();
            Quaternion rotation = pakcet.ReadQuaternion();

            MultiPlayerManager._instance.SpawnPlayer(id, username, position, rotation);
        }

        public static void PlayerPosition(Packet pakcet)
        {
            int id = pakcet.ReadInt();
            Vector3 position = pakcet.ReadVector3();

            // Debug.Log($"Receive {id}'s position from server : {position}");
            if (id != Client.instance.myId)
                MultiPlayerManager.players[id].Player.position = position;
        }

        public static void PlayerRotation(Packet pakcet)
        {
            int id = pakcet.ReadInt();
            Quaternion rotation = pakcet.ReadQuaternion();

            MultiPlayerManager.players[id].Player.rotation = rotation;
        }

        public static void PlayerEmoji(Packet pakcet)
        {
            int id = pakcet.ReadInt();
            int emojiIndex = pakcet.ReadInt();
            
            if (id != Client.instance.myId)
            {
                Debug.Log($"[{emojiIndex}] : receieve emoji from server");
                MultiPlayerManager.players[id].SetEmoji(emojiIndex);
            }
        }

        public static void PlayerDisconnected(Packet pakcet)
        {
            int id = pakcet.ReadInt();

            Destroy(MultiPlayerManager.players[id].gameObject);
            MultiPlayerManager.players.Remove(id);
        }
    }
}