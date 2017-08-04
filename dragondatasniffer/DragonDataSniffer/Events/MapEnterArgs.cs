using DragonDataSniffer.Object;

namespace DragonDataSniffer.Events
{
    public class MapEnterArgs
    {
        public MapEnterArgs(MapObject pMapobject)
        {
            PObject = pMapobject;
        }

        public MapObject PObject { get; private set; }
    }
}