using System.Net;
using UnityEngine;

namespace NHD.Multiplay.ClientSide
{
    ///<summary>
    /// 서버에서 받은 패킷을 처리하는 클래스. 이곳에서 Multiplayer의 함수를 이용합니다.
    ///</summary>
    public class ClientHandle : MonoBehaviour
    {
        public static void Welcome(Packet pakcet)
        {
            string msg = pakcet.ReadString();
            int myId = pakcet.ReadInt();

            Debug.Log($"Message from server : {msg}");
            Client._instance._myId = myId;

            // send welcome received packet
            ClientSend.WelcomeReceived();

            Client._instance._udp.Connect(((IPEndPoint)Client._instance._tcp.socket.Client.LocalEndPoint).Port);
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
            if (id != Client._instance._myId)
                MultiPlayerManager._players[id]._player.position = position;
        }

        public static void PlayerRotation(Packet pakcet)
        {
            int id = pakcet.ReadInt();
            Quaternion rotation = pakcet.ReadQuaternion();

            MultiPlayerManager._players[id]._player.rotation = rotation;
        }

        public static void PlayerEmoji(Packet pakcet)
        {
            int id = pakcet.ReadInt();
            int emojiIndex = pakcet.ReadInt();
            
            if (id != Client._instance._myId)
            {
                // Debug.Log($"[{emojiIndex}] : receieve emoji from server");
                MultiPlayerManager._players[id].SetEmoji(emojiIndex);
            }
        }

        public static void PlayerDisconnected(Packet pakcet)
        {
            int id = pakcet.ReadInt();

            Destroy(MultiPlayerManager._players[id].gameObject);
            MultiPlayerManager._players.Remove(id);
        }
    }
}