namespace DragonDataSniffer.Network.ClientHandler
{
    public class MapEntryHandler

    {
        [ClientPacketHandler(Handler6Type._Header, 1)]
        public static void On_SelectEntry2(GameClient client, FiestaPacket packet)
        {
            if (!packet.TryReadInt16(out short objectID)
                || !packet.TryReadString(out string name, 16)
                || !packet.TryReadInt32(out int k)
                || !packet.TryReadString(out string hash, 1600))
            {
                client.Dispose();
                return;
            }

            using (var mpack = new FiestaPacket(6, 1))
            {
                mpack.WriteInt16(objectID);
                mpack.WriteString(name, 16);
                mpack.WriteInt32(k);
                mpack.WriteString(hash, 1600);
                client.SendPacket(packet);
            }
        }

        [ClientPacketHandler(Handler9Type._Header, Handler9Type.SelectObject)]
        public static void On_SelectEntry(GameClient client, FiestaPacket packet)
        {
            if (!packet.TryReadInt16(out short objectID))
            {
                client.Dispose();
            }
            client.PCharacter.TargetID = objectID;

            client.SClient.SendPacket(packet);
        }

        [ClientPacketHandler(Handler9Type._Header, Handler9Type.DelectObject)]
        public static void On_DelectEntry(GameClient client, FiestaPacket packet)
        {
            client.PCharacter.TargetID = -1;
            client.SClient.SendPacket(packet);
        }
    }
}