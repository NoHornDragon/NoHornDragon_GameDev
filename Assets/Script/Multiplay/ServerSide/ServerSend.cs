namespace NHD.Multiplay.ServerSide
{
    public class ServerSend
    {
        private static void SendTCPData(int _toClient, Packet _packet)
        {
            _packet.WriteLength();
            Server.clients[_toClient].tcp.SendData(_packet);
        }

        private static void SendUDPData(int _toClient, Packet _packet)
        {
            _packet.WriteLength();
            Server.clients[_toClient].udp.SendData(_packet);
        }



        private static void SendTCPDataToAll(Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                Server.clients[i].tcp.SendData(_packet);
            }
        }

        private static void SendTCPDataToAll(int _exceptClient, Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                if (i == _exceptClient) continue;
                Server.clients[i].tcp.SendData(_packet);
            }
        }

        private static void SendUDPDataToAll(Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                Server.clients[i].udp.SendData(_packet);
            }
        }

        private static void SendUDPDataToAll(int _exceptClient, Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                if (i == _exceptClient) continue;
                Server.clients[i].udp.SendData(_packet);
            }
        }

        #region Packets
        public static void Welcome(int _toClient, string _msg)
        {
            using (Packet packet = new Packet((int)ServerPackets.welcome))
            {
                packet.Write(_msg);
                packet.Write(_toClient);

                SendTCPData(_toClient, packet);
            }
        }

        public static void SpawnPlayer(int _toClient, PlayerInfo player)
        {
            using (Packet packet = new Packet((int)ServerPackets.spawnPlayer))
            {
                packet.Write(player._id);
                packet.Write(player._username);
                packet.Write(player._position);
                packet.Write(player._rotation);

                SendTCPData(_toClient, packet);
            }
        }

        public static void PlayerPosition(PlayerInfo player)
        {
            using (Packet packet = new Packet((int)ServerPackets.playerPosition))
            {
                packet.Write(player._id);
                packet.Write(player._position);

                SendUDPDataToAll(packet);
            }
        }

        public static void PlayerRotation(PlayerInfo player)
        {
            using (Packet packet = new Packet((int)ServerPackets.playerPosition))
            {
                packet.Write(player._id);
                packet.Write(player._rotation);

                SendUDPDataToAll(player._id, packet);
            }
        }

        public static void PlayerEmojied(PlayerInfo player)
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