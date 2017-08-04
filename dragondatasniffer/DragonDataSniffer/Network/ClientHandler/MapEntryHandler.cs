using DragonDataSniffer.Network.Networking;

namespace DragonDataSniffer.Network.ClientHandler
{
    public class MapEntryHandler
    {
        [ClientPacketHandler(Handler9Type._Header, Handler9Type.SelectObject)]
        public static void On_SelectEntry(GameClient client, FiestaPacket packet)
        {
            if (!packet.TryReadInt16(out short objectID))
            {
                client.Dispose();
            }
            client.pCharacter.TargetID = objectID;

            client.SClient.SendPacket(packet);
        }
        [ClientPacketHandler(Handler9Type._Header, Handler9Type.DelectObject)]
        public static void On_DelectEntry(GameClient client, FiestaPacket packet)
        {
            client.pCharacter.TargetID = -1;
            client.SClient.SendPacket(packet);
        }
    }
}
