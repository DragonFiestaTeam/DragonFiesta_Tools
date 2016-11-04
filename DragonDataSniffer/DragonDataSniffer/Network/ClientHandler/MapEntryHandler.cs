using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonDataSniffer.Network.ClientHandler
{
    public class MapEntryHandler

    {
        
        [ClientPacketHandler(Handler6Type._Header, 1)]
        public static void On_SelectEntry2(GameClient client, FiestaPacket packet)
        {
            short objectID; string name, hash;
            int k;
            if (!packet.TryReadInt16(out objectID) 
                || !packet.TryReadString(out name, 16) 
                || !packet.TryReadInt32(out k) 
                || !packet.TryReadString(out hash,1600))
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
            short objectID;
            if(!packet.TryReadInt16(out objectID))
            {
                client.Dispose();
            }
            client.pCharacter.TargetID = objectID;

            client.sClient.SendPacket(packet);
        }
        [ClientPacketHandler(Handler9Type._Header, Handler9Type.DelectObject)]
        public static void On_DelectEntry(GameClient client, FiestaPacket packet)
        {
            client.pCharacter.TargetID = -1;
            client.sClient.SendPacket(packet);
        }
    }
}
