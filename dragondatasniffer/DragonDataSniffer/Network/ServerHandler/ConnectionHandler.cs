using DragonDataSniffer.Network.Networking;

namespace DragonDataSniffer.Network.ServerHandler
{
    public sealed class ConnectionHandler
    {
        [ServerPacketHandler(Handler6Type._Header, Handler6Type.ZoneIP)]
        public static void On_ReadZoneServerIP(ServerClient client, FiestaPacket packet)
        {
            if (!packet.TryReadUInt16(out ushort MapID)
                || !packet.TryReadInt32(out int X)
                || !packet.TryReadInt32(out int Y)
                || !packet.TryReadString(out string IP, 16)
                || !packet.TryReadUInt16(out ushort Port)
                || !packet.TryReadUInt16(out ushort unk))
            {
                client.OnDisconnect();
                return;
            }

            GameAcceptorManager.Instance.StopAcceptorByType(ClientType.Zone);
            GameAcceptorManager.Instance.StartAcceptor(ClientType.Zone, Port);

            using (var m = new FiestaPacket(Handler6Type._Header, Handler6Type.ZoneIP))
            {
                m.WriteUInt16(MapID);
                m.WriteInt32(X);
                m.WriteInt32(Y);
                m.WriteString(Config.Instance.TunnelIP, 16);
                m.WriteUInt16(Port);
                m.WriteUInt16(unk);
                client.cClient.SendPacket(m);
            }

        }

        [ServerPacketHandler(Handler4Type._Header, Handler4Type.GameServerIP)]
        public static void On_ReadGameServerIP(ServerClient client, FiestaPacket packet)
        {
            if (!packet.TryReadString(out string GameIP, 16) || !packet.TryReadUInt16(out ushort GamePort))
            {
                client.OnDisconnect();
                return;
            }

            GameAcceptorManager.Instance.StartAcceptor(ClientType.Zone, GamePort);
            using (var p = new FiestaPacket(Handler4Type._Header, Handler4Type.GameServerIP))
            {
                p.WriteString(Config.Instance.TunnelIP, 16);
                p.WriteUInt16(GamePort);
                client.cClient.SendPacket(p);
            }
        }

        [ServerPacketHandler(Handler3Type._Header, Handler3Type.WorldServerIP)]
        public static void On_ReadWorldServerIP(ServerClient client, FiestaPacket packet)
        {
            if (!packet.TryReadByte(out byte worlsstate)
                || !packet.TryReadString(out string worldIP, 16)
                || !packet.TryReadUInt16(out ushort worldport)
                || !packet.TryReadBytes(out byte[] uniq, 64))
            {
                client.OnDisconnect();
                return;
            }

            GameAcceptorManager.Instance.StartAcceptor(ClientType.World, worldport);
            using (var p = new FiestaPacket(Handler3Type._Header, Handler3Type.WorldServerIP))
            {
                p.WriteByte(worlsstate);
                p.WriteString(Config.Instance.TunnelIP, 16);
                p.WriteUInt16(worldport);
                p.WriteBytes(uniq);
                client.cClient.SendPacket(p);
            }

            GameAcceptorManager.Instance.StopAcceptorByType(ClientType.Login);
        }
    }
}