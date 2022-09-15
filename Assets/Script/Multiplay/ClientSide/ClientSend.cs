using UnityEngine;

namespace NHD.Multiplay.ClientSide
{
    public class ClientSend : MonoBehaviour
    {
        private static void SendTCPData(Packet packet)
        {
            packet.WriteLength();
            Client.instance.tcp.SendData(packet);
        }

        private static void SendUDPData(Packet packet)
        {
            packet.WriteLength();
            Client.instance.udp.SendData(packet);
        }

        #region Packets

        public static void WelcomeReceived()
        {
            using (Packet packet = new Packet((int)ClientPackets.welcomeReceived))
            {
                packet.Write(Client.instance.myId);
                packet.Write(UIManager.instance.userNameField.text);

                SendTCPData(packet);
            }
        }

        public static void PlayerMovement(Vector3 position)
        {
            using (Packet packet = new Packet((int)ClientPackets.playerMovement))
            {
                packet.Write(position);
                packet.Write(MultiPlayerManager.players[Client.instance.myId].transform.rotation);

                SendUDPData(packet);
            }

        }

        #endregion
    }
}