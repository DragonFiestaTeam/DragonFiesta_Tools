using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonDataSniffer.Network.ServerHandler
{
    public sealed class Handler2
    {
        //this is incorrect, somehow?
        [ServerPacketHandler(SH2Type.SetXorKeyPosition)]
        public static void On_ReadXorPos(ServerClient client, FiestaPacket packet)
        {
            ushort XorPos;
            if (!packet.TryReadUInt16(out XorPos))
            {
                client.Dispose();
            }

            client.cClient.XorPosition = XorPos;
            client.cClient.Start();
            using (var p = new FiestaPacket(7, 2))
            {
                p.WriteUInt16(XorPos);
                client.cClient.SendPacket(p);
            }
          
        }
    }
}
