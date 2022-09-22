using UnityEngine;

namespace NHD.Multiplay.ClientSide
{
    ///<summary>
    /// 클라이언트에서 서버로 패킷을 보낼 때 사용하는 클래스
    ///</summary>
    public class ClientSend : MonoBehaviour
    {
        private static void SendTCPData(Packet packet)
        {
            packet.WriteLength();
            Client._instance._tcp.SendData(packet);
        }

        private static void SendUDPData(Packet packet)
        {
            packet.WriteLength();
            Client._instance._udp.SendData(packet);
        }

        #region Packets

        public static void WelcomeReceived()
        {
            using (Packet packet = new Packet((int)ClientPackets.welcomeReceived))
            {
                packet.Write(Client._instance._myId);
                packet.Write(ServerConnectUIManager._instance._userNameField.text);

                SendTCPData(packet);
            }
        }

        public static void PlayerMovement(Vector3 position)
        {
            using (Packet packet = new Packet((int)ClientPackets.playerMovement))
            {
                packet.Write(position);
                packet.Write(MultiPlayerManager._players[Client._instance._myId].transform.rotation);

                SendUDPData(packet);
            }

        }

        public static void PlayerSendEmoji(int emojiIndex)
        {
            using (Packet packet = new Packet((int)ClientPackets.playerEmoji))
            {
                packet.Write(emojiIndex);
                
                SendUDPData(packet);
            }
        }

        #endregion
    }
}