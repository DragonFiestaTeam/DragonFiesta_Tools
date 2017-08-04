using DragonDataSniffer.Object;
using DragonDataSniffer.Manager;
using DragonDataSniffer.Data;
using DragonDataSniffer.Network.Networking;

namespace DragonDataSniffer.Network.ServerHandler
{
    public class CharacterHandler
    {
        [ServerPacketHandler(Handler4Type._Header, Handler4Type.Chardeatiledinfo)]
        public static void CharDetailedInfo(ServerClient client, FiestaPacket pPacket)
        {
            if (!pPacket.ReadSkip(66) ||
            !pPacket.TryReadString(out string MapName, 12))
            {
                return;
            }

            if (MapDataManager.Instance.GetMapInfoByName(MapName, out MapInfo inf))
            {
                Character pChar = new Character(inf.ID);
                client.pCharacter = pChar;
                client.CClient.pCharacter = pChar;
                if (MapDataManager.Instance.GetMapByID(inf.ID, out Map pMap))
                {
                    pMap.Invoke(pChar);
                }
            }
            client.CClient.SendPacket(pPacket);
        }
    }
}
