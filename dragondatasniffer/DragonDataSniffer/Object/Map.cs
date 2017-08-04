using System;
using System.Collections.Generic;
using DragonDataSniffer.Data;
using DragonDataSniffer.Events;
namespace DragonDataSniffer.Object
{
    public sealed class Map
    {
        public MapInfo PMapInfo { get; private set; }
        public event EventHandler<MapEnterArgs> ObjectEnterTheMap;
        public List<NPC> NPCsByMob = new List<NPC>();
        public Dictionary<ushort, MapObject> MapObjects = new Dictionary<ushort, MapObject>();
        public Map(MapInfo pMapInf)
        {
            PMapInfo = pMapInf;
            ObjectEnterTheMap += Map_ObjectEnterTheMap;
        }

        private void Map_ObjectEnterTheMap(object sender, MapEnterArgs e)
        {
            ushort ObjectID = e.PObject.MapObjectID;
            if (e.PObject is NPC)
            {
                NPC pNPC = e.PObject as NPC;
                NPC OldNPC = NPCsByMob.Find(m => m.MobID == pNPC.MobID && m.MapID == pNPC.MapID && m.X == pNPC.X && m.Y == pNPC.Y);

                if(OldNPC != null)
                {
                    if(MapObjects.ContainsKey(OldNPC.MapObjectID) && pNPC.MapObjectID != OldNPC.MapObjectID)
                    {
                        if (MapObjects.Remove(OldNPC.MapObjectID))
                        {
                            ObjectID = pNPC.MapObjectID;
                        }
                    }
                }
                else
                {
                    Log.WriteLine(LogLevel.Info, "Saveing NPC {0}", pNPC.MobID);
                    pNPC.AddToDB();
                    NPCsByMob.Add(pNPC);
                }
            }
            else if (e.PObject is Character)
            {
                Character pChar = e.PObject as Character;
                Log.WriteLine(LogLevel.Info, "Character Enter The Map");
            }

            if (!MapObjects.ContainsKey(ObjectID))
            {
                MapObjects.Add(ObjectID, e.PObject);
            }
        }

        public void Invoke(MapObject pObject)
        {
            ObjectEnterTheMap.Invoke(null, new MapEnterArgs(pObject));
        }
        public bool MobIDContains(ushort MobID)
        {
            NPC pInfo = NPCsByMob.Find(m => m.MobID == MobID);

            if (pInfo != null)
            {
                return true;
            }
            return false;
        }     
    }
}
