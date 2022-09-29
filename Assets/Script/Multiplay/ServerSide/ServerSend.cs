namespace NHD.Multiplay.ServerSide
{
    ///<summary>
    /// 서버 측에서 각 사용자들에게 데이터를 보내는 클래스
    ///</summary>
    public class ServerSend
    {
        private static void SendTCPData(int toClient, Packet packet)
        {
            packet.WriteLength();
            Server._clients[toClient]._tcp.SendData(packet);
        }

        private static void SendUDPData(int toClient, Packet packet)
        {
            packet.WriteLength();
            Server._clients[toClient]._udp.SendData(packet);
        }



        private static void SendTCPDataToAll(Packet packet)
        {
            packet.WriteLength();
            for (int i = 1; i <= Server._maxPlayers; i++)
            {
                Server._clients[i]._tcp.SendData(packet);
            }
        }

        private static void SendTCPDataToAll(int _exceptClient, Packet packet)
        {
            packet.WriteLength();
            for (int i = 1; i <= Server._maxPlayers; i++)
            {
                if (i == _exceptClient) continue;
                Server._clients[i]._tcp.SendData(packet);
            }
        }

        private static void SendUDPDataToAll(Packet packet)
        {
            packet.WriteLength();
            for (int i = 1; i <= Server._maxPlayers; i++)
            {
                Server._clients[i]._udp.SendData(packet);
            }
        }

        private static void SendUDPDataToAll(int _exceptClient, Packet packet)
        {
            packet.WriteLength();
            for (int i = 1; i <= Server._maxPlayers; i++)
            {
                if (i == _exceptClient) continue;
                Server._clients[i]._udp.SendData(packet);
            }
        }

        #region Packets
        public static void Welcome(int toClient, string _msg)
        {
            using (Packet packet = new Packet((int)ServerPackets.welcome))
            {
                packet.Write(_msg);
                packet.Write(toClient);

                SendTCPData(toClient, packet);
            }
        }

        public static void SpawnPlayer(int toClient, PlayerTrackerInServer player)
        {
            using (Packet packet = new Packet((int)ServerPackets.spawnPlayer))
            {
                packet.Write(player._id);
                packet.Write(player._username);
                packet.Write(player._position);
                packet.Write(player._rotation);

                SendTCPData(toClient, packet);
            }
        }

        public static void PlayerPosition(PlayerTrackerInServer player)
        {
            using (Packet packet = new Packet((int)ServerPackets.playerPosition))
            {
                packet.Write(player._id);
                packet.Write(player._position);

                SendUDPDataToAll(packet);
            }
        }

        public static void PlayerRotation(PlayerTrackerInServer player)
        {
            using (Packet packet = new Packet((int)ServerPackets.playerPosition))
            {
                packet.Write(player._id);
                packet.Write(player._rotation);

                SendUDPDataToAll(player._id, packet);
            }
        }

        public static void PlayerEmojied(PlayerTrackerInServer player)
        {
            using (Packet packet = new Packet((int)ServerPackets.playerEmoji))
            {
                packet.Write(player._id);
                packet.Write(player._emojiIndex);

                SendUDPDataToAll(player._id, packet);
            }
        }

        public static void PlayerDisconnected(int playerId)
        {
            using (Packet packet = new Packet((int)ServerPackets.playerDisconnected))
            {
                packet.Write(playerId);

                SendTCPDataToAll(packet);
            }
        }
        #endregion
    }
}