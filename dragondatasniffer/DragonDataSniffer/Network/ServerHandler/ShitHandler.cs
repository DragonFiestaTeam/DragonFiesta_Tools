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
            client.CClient.SendPacket(pPacket);
        }
        [ServerPacketHandler(Handler4Type._Header, Handler4Type.unk1)]
        public static void Unk1(ServerClient client, FiestaPacket pPacket)
        {
            client.CClient.SendPacket(pPacket);
        }
        [ServerPacketHandler(Handler4Type._Header, Handler4Type.unk2)]
        public static void Unk2(ServerClient client, FiestaPacket pPacket)
        {
            client.CClient.SendPacket(pPacket);
        }
        [ServerPacketHandler(Handler4Type._Header, Handler4Type.CharacterActiveSkillList)]
        public static void ActiveSkills(ServerClient client, FiestaPacket pPacket)
        {
            client.CClient.SendPacket(pPacket);
        }
        [ServerPacketHandler(Handler4Type._Header, Handler4Type.CharacterPassiveSkillList)]
        public static void SSkills(ServerClient client, FiestaPacket pPacket)
        {
            client.CClient.SendPacket(pPacket);
        }
        [ServerPacketHandler(Handler4Type._Header, Handler4Type.CharacterItemList)]
        public static void Itemlist(ServerClient client, FiestaPacket pPacket)
        {
            client.CClient.SendPacket(pPacket);
        }
        [ServerPacketHandler(Handler4Type._Header, Handler4Type.CharacterTimedItemList)]
        public static void Time(ServerClient client, FiestaPacket pPacket)
        {
            client.CClient.SendPacket(pPacket);
        }
        [ServerPacketHandler(Handler4Type._Header, Handler4Type.CharacterTitles)]
        public static void Timezlk(ServerClient client, FiestaPacket pPacket)
        {
            client.CClient.SendPacket(pPacket);
        }
        [ServerPacketHandler(Handler4Type._Header, Handler4Type.CharacterInfoEnd)]
        public static void Chunkendk(ServerClient client, FiestaPacket pPacket)
        {
            client.CClient.SendPacket(pPacket);
        }
        [ServerPacketHandler(Handler6Type._Header, Handler6Type.DetailedCharacterInfo)]
        public static void Deatailedinfo(ServerClient client, FiestaPacket pPacket)
        {
            client.CClient.SendPacket(pPacket);
        }
    }
}
