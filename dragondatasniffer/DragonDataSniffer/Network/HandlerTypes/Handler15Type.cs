namespace DragonDataSniffer.Network
{
    public class Handler15Type : PacketHeader
    {
        public new const byte _Header = 15;

        public const byte SoulStone = 5;
        public const byte Storage = 8;
        public const byte NPCItemList = 9;
        public const byte SkillNPCStuff = 10;
        public const byte StuffNPCItemList = 11;
        public const byte MenuGuild = 12;
    }
}
