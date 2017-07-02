using DragonDataSniffer.Data;
using DragonDataSniffer.Network;
using System;
using System.Collections.Concurrent;

namespace DragonDataSniffer.Object
{
    public class NPC : MapObject
    {
        public ushort MapID { get; set; }
        public ushort MobID { get; private set; }
        public MobInfo PInfo { get; set; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public byte Rot { get; private set; }
        public bool HasMenu { get; private set; }
        public NPCRole Role { get; set; }
        public NPCArgument Argument { get; set; }
        public byte IsGate { get; private set; }
        public string GateName { get; private set; }
        public ConcurrentDictionary<byte, NPCItem> ItemList { get; private set; }

        public NPC(SQLResult pRes, int i = 0) : base()
        {
            ItemList = new ConcurrentDictionary<byte, NPCItem>();

            MobID = pRes.Read<ushort>(i, "MobID");
            MapID = pRes.Read<ushort>(i, "MapID");
            X = pRes.Read<int>(i, "X");
            Y = pRes.Read<int>(i, "Y");
            Rot = pRes.Read<byte>(i, "Rotation");
            HasMenu = pRes.Read<bool>(i, "HasMenu");
            Role = (NPCRole)pRes.Read<byte>(i, "Role");
            Argument = (NPCArgument)pRes.Read<byte>(i, "RoleArgument");
        }

        public NPC(FiestaPacket packet, ushort pMapID) : base()
        {
            try
            {
                string pGateName = "";
                NPCRole role = NPCRole.None;
                if (!packet.TryReadUInt16(out ushort pObjectID)
                      || !packet.TryReadByte(out byte pUnk)
                      || !packet.TryReadUInt16(out ushort pMobID)
                      || !packet.TryReadInt32(out int pX)
                      || !packet.TryReadInt32(out int pY)
                      || !packet.TryReadByte(out byte pRot)
                      || !packet.TryReadByte(out byte pIsGate))
                {
                    return;
                }

                if (pIsGate == 1)
                {
                    Role = NPCRole.Gate;
                    packet.TryReadString(out pGateName, 12);
                    packet.ReadSkip(125);
                }
                else
                {
                    packet.ReadSkip(137);
                }

                ItemList = new ConcurrentDictionary<byte, NPCItem>();
                MapObjectID = pObjectID;
                MobID = pMobID;
                MapID = pMapID;
                X = pX;
                Y = pY;
                Rot = pRot;
                IsGate = pIsGate;
                GateName = pGateName;
                Role = role;

                if (Manager.NPCDataManager.Instance.NPCByID.TryGetValue(pMobID, out MobInfo mInfo))
                {
                    PInfo = mInfo;
                }
            }
            catch
            {
            }
        }

        public bool AddItem(NPCItem pItem, bool Save = false)
        {
            if (ItemList.TryAdd(pItem.Slot, pItem))
            {
                if (Save)
                {
                    pItem.AddToDB();
                }

                return true;
            }

            return false;
        }

        public void UpdateArgument()
        {
            string update = "UPDATE NPCTable SET RoleArgument = '" + (byte)Argument + "',Role = '" + (byte)Role + "' WHERE MobID = '" + MobID + "'";
            DatabaseManager.RunSQL(update);
        }

        public void AddToDB()
        {
            string NpcString = "INSERT INTO NPCTable (MobID,MapID,X,Y,Rotation,HasMenu,Role,RoleArgument)" +
               "VALUES ("
                  + "'" + MobID + "',"
                  + "'" + MapID + "',"
                  + "'" + X + "',"
                  + "'" + Y + "',"
                  + "'" + Rot + "',"
                  + "'" + Convert.ToByte(HasMenu) + "',"
                  + "'" + (byte)Role + "',"
                  + "'" + (byte)Argument + "')";

            DatabaseManager.RunSQL(NpcString);
        }
    }
}