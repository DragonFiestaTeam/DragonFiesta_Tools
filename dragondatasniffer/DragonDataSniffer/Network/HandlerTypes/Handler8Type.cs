namespace DragonDataSniffer.Network
{
    public class Handler8Type : PacketHeader
    {
        public new const byte _Header = 8;

        public const byte ChatNormalReqest = 1;
        public const byte ChatNormal = 2;
        public const byte YellowMessage = 31;
        public const byte GMNote = 17;
    }
}
