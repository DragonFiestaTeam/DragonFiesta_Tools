using System;
using FiestaLib.Networking;
using FiestaLib.Data;

namespace FiestaTunnel.Handler
{
    internal class ConnectionHandler
    {
        [PacketHandler((ushort)ServerOpCode.WorldServerIP)]
        public static void HandleWorldConnection(LinkedClient pClient, Packet pPacket)
        {
            Packet packet = pPacket;

            Console.WriteLine("WorldTransfer");
            if (!packet.TryReadByte(out byte worldserverstatus) ||
!packet.TryReadString(out string worldserverip, 16) ||
!packet.TryReadShort(out short worldport))
            {
                Console.WriteLine("Error parsing world_ip packet.");
                return;
            }
            Program.CreateListener(worldserverip, worldport);
            packet.SetOffset(3);
            packet.WriteString(pClient.ProxyIP, 16);
            pClient.inClient.SendPacket(packet);

        }

        [PacketHandler((ushort)ServerOpCode.GameServerIP)]
        public static void HandleGameConnection(LinkedClient pClient, Packet pPacket)
        {
            Packet packet = pPacket;


            Console.WriteLine("GameTransfer.");
            if (!packet.TryReadString(out string gameip, 16) ||
!packet.TryReadShort(out short gameport))
            {
                Console.WriteLine("Error parsing game_ip packet.");
                return;
            }
            Program.CreateListener(gameip, gameport);
            packet.SetOffset(2);
            packet.WriteString(pClient.ProxyIP, 16);
            pClient.inClient.SendPacket(packet);
            Program.map = new Map();
        }

        [PacketHandler((ushort)ServerOpCode.ZoneIP)]
        public static void HandleZoneConnection(LinkedClient pClient, Packet pPacket)
        {
            Packet packet = pPacket;


            Console.WriteLine("ZoneTransfer");
            if (!packet.TryReadShort(out short mapid) ||
    !packet.TryReadInt(out int xmap) ||
    !packet.TryReadInt(out int ymap) ||
    !packet.TryReadString(out string zoneip, 16) ||
    !packet.TryReadShort(out short zoneport))
            {
                Console.WriteLine("Error reading zone transfer.");
                return;
            }
            Program.CreateListener(zoneip, zoneport);
            packet.SetOffset(12);
            packet.WriteString(zoneip, 16);
            pClient.inClient.SendPacket(packet);
            Program.map = new Map(mapid);
        }

    }
}
