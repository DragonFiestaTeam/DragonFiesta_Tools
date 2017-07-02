using FiestaLib.Networking;
using FiestaLib.Util.Database;

namespace FiestaLib.Objects
{
    public class NPC
    {
        public int Map;
        public ushort NpcId;
        public int x;
        public int y;
        public byte rot;
        public byte isGate;
        public string GateName;

        public static NPC GetNpcFromPacket(Packet packet)
        {
            string pGateName = "";

            if (!packet.TryReadUShort(out ushort pObjectID)
                || !packet.TryReadByte(out byte pUnk)
                || !packet.TryReadUShort(out ushort pNpcId)
                || !packet.TryReadInt(out int pX)
                || !packet.TryReadInt(out int pY)
                || !packet.TryReadByte(out byte pRot)
                || !packet.TryReadByte(out byte pIsGate))
                return null;

            if (pIsGate == 1)
            {
                packet.TryReadString(out pGateName, 12);
                packet.ReadSkip(125);
            }
            else
            {
                packet.ReadSkip(137);
            }

            NPC spawnm = new NPC
            {
                NpcId = pNpcId,
                x = pX,
                y = pY,
                rot = pRot,
                isGate = pIsGate,
                GateName = pGateName,
                Map = 0
            };

            return spawnm;
        }

        public void AddToDB()
        {
            string npcString = "INSERT INTO spawnpoins (MobID,MapID,PosX,PosY,Rotation)" +
                                                  "VALUES ("
                                                  + NpcId + ","
                                                  + Map + ","
                                                  + x + ","
                                                  + y + ","
                                                  + rot + ")";

            DatabaseHelper.Instance.runSQL(npcString);
        }
    }
}
