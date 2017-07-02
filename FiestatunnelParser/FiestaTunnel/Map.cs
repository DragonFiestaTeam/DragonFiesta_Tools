using System.Collections.Generic;

namespace FiestaTunnel
{
    public class Map
    {
        public Dictionary<ushort, string> Characters = new Dictionary<ushort, string>();

        public short MapID { get; set; }

        public Map() { }

        public Map(short mapid)
        {
            MapID = mapid;
        }
    }
}
