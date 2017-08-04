using DragonDataSniffer.Network.Networking;

namespace DragonDataSniffer.Network.ServerHandler
{
    public class ShitHandler
    {
        [ServerPacketHandler(Handler4Type._Header, Handler4Type.CharacterLook)]
        public static void CharacterLook(ServerClient client, FiestaPacket pPacket)
        {
        }
        [ServerPacketHandler(Handler4Type._Header, Handler4Type.CharacterQuestsBusy)]
        public static void CharacterQuestBusy(ServerClient client, FiestaPacket pPacket)
        {
        }
        [ServerPacketHandler(Handler4Type._Header, Handler4Type.CharacterQuestsDone)]
        public static void CharacterQuestDone(ServerClient client, FiestaPacket pPacket)
        {
            client.cClient.SendPacket(pPacket);
        }
        [ServerPacketHandler(Handler4Type._Header, Handler4Type.unk1)]
        public static void unk1(ServerClient client, FiestaPacket pPacket)
        {
            client.cClient.SendPacket(pPacket);
        }
        [ServerPacketHandler(Handler4Type._Header, Handler4Type.unk2)]
        public static void unk2(ServerClient client, FiestaPacket pPacket)
        {
            client.cClient.SendPacket(pPacket);
        }
        [ServerPacketHandler(Handler4Type._Header, Handler4Type.CharacterActiveSkillList)]
        public static void activeSkills(ServerClient client, FiestaPacket pPacket)
        {
            client.cClient.SendPacket(pPacket);
        }
        [ServerPacketHandler(Handler4Type._Header, Handler4Type.CharacterPassiveSkillList)]
        public static void sSkills(ServerClient client, FiestaPacket pPacket)
        {
            client.cClient.SendPacket(pPacket);
        }
        [ServerPacketHandler(Handler4Type._Header, Handler4Type.CharacterItemList)]
        public static void Itemlist(ServerClient client, FiestaPacket pPacket)
        {
            client.cClient.SendPacket(pPacket);
        }
        [ServerPacketHandler(Handler4Type._Header, Handler4Type.CharacterTimedItemList)]
        public static void time(ServerClient client, FiestaPacket pPacket)
        {
            client.cClient.SendPacket(pPacket);
        }
        [ServerPacketHandler(Handler4Type._Header, Handler4Type.CharacterTitles)]
        public static void timezlk(ServerClient client, FiestaPacket pPacket)
        {
            client.cClient.SendPacket(pPacket);
        }
        [ServerPacketHandler(Handler4Type._Header, Handler4Type.CharacterInfoEnd)]
        public static void chunkendk(ServerClient client, FiestaPacket pPacket)
        {
            client.cClient.SendPacket(pPacket);
        }
        [ServerPacketHandler(Handler6Type._Header, Handler6Type.DetailedCharacterInfo)]
        public static void Deatailedinfo(ServerClient client, FiestaPacket pPacket)
        {
            client.cClient.SendPacket(pPacket);
        }
    }
}
