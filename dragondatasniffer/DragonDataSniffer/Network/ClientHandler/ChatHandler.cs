using DragonDataSniffer.Chat;
using DragonDataSniffer.Network.Networking;

namespace DragonDataSniffer.Network.ClientHandler
{
    public class ChatHandler
    {
        [ClientPacketHandler(Handler8Type._Header, Handler8Type.ChatNormalReqest)]
        public static void On_ReadChatNormalrequest(GameClient client, FiestaPacket packet)
        {
            if (!packet.TryReadByte(out byte unk) || !packet.TryReadByte(out byte msglen) || !packet.TryReadString(out string msg, msglen))
            {
                client.Dispose();
                return;
            }

            if (msg.StartsWith("!"))
            {
                string[] cmd = msg.Split(' ');
                CommandHandler.Instance.ExecuteCommand(cmd);
            }
            else
            {
                client.SClient.SendPacket(packet);
            }
        }
    }
}
