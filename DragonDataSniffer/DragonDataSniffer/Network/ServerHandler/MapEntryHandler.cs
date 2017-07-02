using DragonDataSniffer.Manager;
using DragonDataSniffer.Object;

namespace DragonDataSniffer.Network.ServerHandler
{
    public class MapEntryHandler
    {
        [ServerPacketHandler(Handler7Type._Header, Handler7Type.SpawnSingleObject)]
        public static void On_SpawnSingleObject(ServerClient client, FiestaPacket packet)
        {
            NPC pNPC = new NPC(packet, client.PCharacter.MapID);

            if (!NPCDataManager.Instance.IsNPCContains(pNPC.MobID))//check is npc
            {
                return;
            }
            if (MapDataManager.Instance.GetMapByID(client.PCharacter.MapID, out Map pMap))
            {
                pMap.Invoke(pNPC);
            }
            client.CClient.SendPacket(packet);
        }

        [ServerPacketHandler(Handler7Type._Header, Handler7Type.SpawnMultiObject)]
        public static void On_SpawnMultiObject(ServerClient client, FiestaPacket packet)
        {
            if (!packet.TryReadByte(out byte pCount))
            {
                client.Dispose();
                return;
            }

            for (int i = 0; i < pCount; i++)
            {
                NPC pNPC = new NPC(packet, client.PCharacter.MapID);

                if (!NPCDataManager.Instance.IsNPCContains(pNPC.MobID))//check is npc
                {
                    continue;
                }
                if (MapDataManager.Instance.GetMapByID(client.PCharacter.MapID, out Map pMap))
                {
                    pMap.Invoke(pNPC);
                }
            }
            client.CClient.SendPacket(packet);
        }
    }
}